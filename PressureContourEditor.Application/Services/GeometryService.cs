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

        public Result<Dictionary<DoubleParametersRole,double>> CalculateParameters(
            IPunchingContourParameters parameters, 
            List<IntersectionPoint> intersectionPoints)
        {
            throw new NotImplementedException();
        }
    }
}
