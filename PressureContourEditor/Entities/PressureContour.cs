using PressureContourEditor.Domain.Abstraction;
using PressureContourEditor.Domain.Exceptions;
using PressureContourEditor.Domain.GeometryPrimitives;
using System.Collections.Generic;

namespace PressureContourEditor.Domain.Entities
{
    public class PressureContour 
    {        
        public PressureContourType Type { get; private set; }
        public HashSet<ContourSideName> ActiveSides { get; private set; }
        public Dictionary<DimensionsRole,double> Dimensions { get; set; }
        public double H0 { get; }
        public Dictionary<(ContourSideName, PressureContourParametersRole), double> Parameters { get; set; }      

        public GeometryContour ContourHalfH0 { get; }
        public GeometryContour Contour6H0 { get; }        

       

        public PressureContour(            
            PressureContourType pressureContourType,            
            Dictionary<DimensionsRole,double> dimensions,
            double h0,
            Dictionary<(ContourSideName, PressureContourParametersRole), double> parameters)
        {            
            Type = pressureContourType;
            ActiveSides = new HashSet<ContourSideName>();
            switch (Type)
            {
                case PressureContourType.EndWall:
                    ActiveSides.Add(ContourSideName.Left);
                    ActiveSides.Add(ContourSideName.Bottom);
                    ActiveSides.Add(ContourSideName.Right);                                        
                    break;
                case PressureContourType.WallCorner:
                    ActiveSides.Add(ContourSideName.Bottom);
                    ActiveSides.Add(ContourSideName.Right);
                    break;
                case PressureContourType.Pylon:
                    ActiveSides.Add(ContourSideName.Top);
                    ActiveSides.Add(ContourSideName.Left);
                    ActiveSides.Add(ContourSideName.Bottom);
                    ActiveSides.Add(ContourSideName.Right);
                    break;

            }            
            Dimensions = dimensions ?? throw new CreateContourException("При создании контура параметр dimensions is null");
            switch (Type)
            {
                case PressureContourType.EndWall:
                    if (!Dimensions.ContainsKey(DimensionsRole.Thickness))
                        throw new CreateContourException("При создании контура типа торец стены не получен параметер Толщина стены - thickness");
                    if (Dimensions[DimensionsRole.Thickness]<=0)
                        throw new CreateContourException("При создании контура типа торец стены Толщина стены должна быть больше ноля");
                    break;
                case PressureContourType.WallCorner:
                    string message=String.Empty;
                    if (!Dimensions.ContainsKey(DimensionsRole.Thickness))
                        message+="При создании контура типа угол стены не получен параметер Толщина первой стены - thickness\n";
                    if (!Dimensions.ContainsKey(DimensionsRole.Thickness2))
                        message += "При создании контура типа угол стены не получен параметер Толщина второй стены - thickness2\n";
                    if (!String.IsNullOrEmpty(message))
                        throw new CreateContourException(message);
                    if (Dimensions[DimensionsRole.Thickness] <= 0)
                        throw new CreateContourException("При создании контура типа Угол стены Толщина первой стены должна быть больше ноля");
                    if (Dimensions[DimensionsRole.Thickness] <= 0)
                        throw new CreateContourException("При создании контура типа угол стены Толщина стены должна быть больше ноля");
                    break;
                case PressureContourType.Pylon:
                    message=String.Empty;
                    if (!Dimensions.ContainsKey(DimensionsRole.Thickness))
                        message+="При создании контура типа пилон не получен параметер Толщина пилона - thickness\n";
                    if (!Dimensions.ContainsKey(DimensionsRole.PylonLength))
                        message += "При создании контура типа пилон не получен параметер Длина пилона - PylonLength\n";
                    if (!String.IsNullOrEmpty(message))
                        throw new CreateContourException(message);
                    break;
            }
            if (h0<=0)
                throw new CreateContourException("Параметр H0 должен быть больше ноля");
            H0 = h0;
            Parameters = parameters ?? throw new CreateContourException("При создании контура параметр parameters is null");                    
            ContourHalfH0 = CreateContour( 0.5 * h0);
            Contour6H0 = CreateContour(6 * h0);
        }

        private GeometryContour CreateContour(double offset)
        {
            Point2D bottomRight;
            Point2D bottomLeft;
            Point2D topRight;
            Point2D topLeft;
            Point2D center;
            double h0 = H0;
            double thickness = Dimensions[DimensionsRole.Thickness];

            switch (Type)
            {
                case PressureContourType.EndWall:

                    bottomRight = new Point2D(thickness / 2 + offset, offset);
                    bottomLeft = new Point2D(thickness / 2 - offset, -offset);
                    topRight = new Point2D(thickness / 2 + offset, thickness + 0.5 * h0);
                    topLeft = new Point2D(-thickness / 2 - offset, thickness + 0.5 * h0);
                    center = new Point2D(0, (thickness + 0.5 * h0) * 0.5);
                    break;
                case PressureContourType.WallCorner:
                    double thicness2 = Dimensions[DimensionsRole.Thickness2];
                    bottomRight = new Point2D(thickness / 2 + offset, offset);
                    bottomLeft = new Point2D(thickness + thicness2 - thickness / 2 - offset, -offset);
                    topRight = new Point2D(thickness / 2 + offset, thickness + thicness2);
                    topLeft = new Point2D(thickness + thicness2 - thickness / 2 - offset, thickness + thicness2);
                    center = new Point2D(0, (thickness + 0.5 * h0) * 0.5);
                    break;
                case PressureContourType.Pylon:
                    double length = Dimensions[DimensionsRole.PylonLength];
                    bottomRight = new Point2D(thickness / 2 + offset, -length / 2 - offset);
                    bottomLeft = new Point2D(-thickness / 2 - offset, -length / 2 - offset);
                    topRight = new Point2D(thickness / 2 + offset, length / 2 + offset);
                    topLeft = new Point2D(-thickness / 2 - offset, length / 2 + offset);
                    center = new Point2D(0, 0);
                    break;
                default:
                    return new GeometryContour($"{Type} не поддерживается");

            }

            Line2D[] contourLines = new Line2D[4];
            contourLines[0] = new Line2D(topRight, topLeft);
            contourLines[1] = new Line2D(topLeft, bottomLeft);
            contourLines[2] = new Line2D(bottomLeft, bottomRight);
            contourLines[3] = new Line2D(bottomRight, topRight);

            var contour = new GeometryContour();
            var values = Enum.GetValues(typeof(ContourSideName));

            for (var i = 0; i < contourLines.Length; i++)
            {
                if (!contour.TryAddItem((ContourSideName)values.GetValue(i), contourLines[i]))
                    return new GeometryContour($"Не удалорсь добавить сторону {(ContourSideName)values.GetValue(i)}");
            }

            return contour;

        }
    }
}
