using CSharpFunctionalExtensions;
using PressureContourEditor.Domain.Abstraction;
using PressureContourEditor.Domain.Entities;
using PressureContourEditor.Domain.GeometryPrimitives;

namespace PressureContourEditor.Domain.Services
{
    public class GeometryService : IGeometryService
    {
        /// <summary>
        /// Находит точки пересечения двух секущих линий с геометрическим контуром
        /// </summary>
        /// <param name="lines">секущие линии</param>
        /// <param name="contour">геометрический контур</param>
        /// <returns></returns>
        public Result<List<IntersectionPoint>> LineWithContourIntersection(IntersectionLines lines, GeometryContour contour)
        {
            if (lines == null)
                return Result.Failure<List<IntersectionPoint>>("IntersectionLines are null");
            if (contour == null)
                return Result.Failure<List<IntersectionPoint>>("Contour is null");
            List<IntersectionPoint> points = new List<IntersectionPoint>();
            foreach (var p in contour)
            {
                Point2D? point;
                if (p.Value.IntersectWithLine(lines.FirstLine, out point))
                    points.Add(new IntersectionPoint((Point2D)point, p.Key));
                if (p.Value.IntersectWithLine(lines.SecondLine, out point))
                    points.Add(new IntersectionPoint((Point2D)point, p.Key));
            }
            return points;
        }

        public Result<Dictionary<(ContourSideName, PressureContourParametersRole), double>> CalculateParameters(
            GeometryContour contourHalfH0,
            List<IntersectionPoint> intersectionPoints)
        {            
            var point1 = intersectionPoints[0];
            var point2 = intersectionPoints[1];
            var result = new Dictionary<(ContourSideName, PressureContourParametersRole), double>();            

            // секущие точки на одной стороне - устанавливаем отверстие
            if (point1.SideName == point2.SideName)
            {
                foreach (var side in contourHalfH0)
                {
                    if (side.Key != point1.SideName)
                        continue;
                    else
                    {
                        var offsetHole = Math.Min(
                            side.Value.StartPoint.DistanceTo(point1.Point),
                            side.Value.StartPoint.DistanceTo(point2.Point));
                        var hole = point1.Point.DistanceTo(point2.Point);
                        result.Add((side.Key, PressureContourParametersRole.HoleOffsetFromStart), offsetHole);
                        result.Add((side.Key, PressureContourParametersRole.HoleWidth), hole);
                        return result;
                    }
                }
            }
            // секущие точки на разных сторонах
            else 
            {
                bool firstSideIsFounded = false;
                foreach(var side in contourHalfH0)
                {
                    // сторона первой точки не найдена, первая точка не на текущей стороне
                    // ничего не делаем
                    if (!firstSideIsFounded && point1.SideName != side.Key)
                        continue;
                    // сторона первой точки не найдена, первая точка на текущей стороне
                    // устанавливаем отступ от точки до конца линии
                    if (!firstSideIsFounded && point1.SideName == side.Key)
                    {
                        firstSideIsFounded = true;
                        double offsetFromEnd = point1.Point.DistanceTo(side.Value.EndPoint);
                        result.Add((side.Key,PressureContourParametersRole.OffsetFromEnd), offsetFromEnd);
                        continue;
                    }
                    // сторона первой точки найдена, вторая точка не на текщей стороне
                    // выключаем сторону
                    if (firstSideIsFounded && point2.SideName != side.Key)
                    {
                        result.Add((side.Key, PressureContourParametersRole.SideIsOn), 0.0);
                        continue;
                    }
                    // сторона первой точки найдена, вторая точка на текщей стороне
                    // устанавливаем отступ от начала линии до точки
                    if (firstSideIsFounded && point2.SideName == side.Key)
                    {
                        var offset = point2.Point.DistanceTo(side.Value.StartPoint);
                        result.Add((side.Key, PressureContourParametersRole.OffsetFromStart),offset);                        
                    }
                }                
            }
            return result;
        }
    }
}
