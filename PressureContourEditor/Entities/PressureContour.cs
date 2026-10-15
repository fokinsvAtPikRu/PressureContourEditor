using CSharpFunctionalExtensions;
using PressureContourEditor.Domain.Exceptions;
using PressureContourEditor.Domain.GeometryPrimitives;
using PressureContourEditor.Domain.ValueObjects;


namespace PressureContourEditor.Domain.Entities
{
    public abstract class PressureContour 
    {        
        public PressureContourType Type { get; set; }
        public HashSet<ContourSideName> ActiveSides { get; set; }
        public Dictionary<DimensionsRole,double> Dimensions { get; set; }
        public double H0 { get; set; }
        public PressureContourParameters Parameters { get; set; }      

        public GeometryContour ContourHalfH0 { get; }
        public GeometryContour Contour6H0 { get; }        

       

        private protected PressureContour(            
            PressureContourType pressureContourType,            
            Dictionary<DimensionsRole,double> dimensions,
            double h0,
            Dictionary<ParameterKey, double> parameters)
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

        public virtual Result<PressureContour> Create(
            PressureContourType pressureContourType,
            Dictionary<DimensionsRole, double> dimensions,
            double h0,
            Dictionary<ParameterKey, double> parameters)
        {
            var result = Validate(pressureContourType, dimensions, h0, parameters);
            if (result.IsFailure)
                return Result.Failure<PressureContour>(result.Error);
            PressureContour(pressureContourType, dimensions, h0, parameters);



        }



        private virtual Result Validate(
            PressureContourType pressureContourType,
            Dictionary<DimensionsRole, double> dimensions,
            double h0,
            Dictionary<ParameterKey, double> parameters)
        {
            string errorMessage = String.Empty;
            if (pressureContourType != PressureContourType.Pylon)
                errorMessage += "Неверный тип контура\n";
            if (dimensions == null)
                errorMessage += "Коллекция размеров не создана\n";
            else
            {
                if (!Dimensions.ContainsKey(DimensionsRole.Thickness))
                    errorMessage += "При создании контура типа торец стены не получен параметер Толщина стены - thickness\n";
                if (Dimensions[DimensionsRole.Thickness] <= 0)
                    errorMessage += "При создании контура типа торец стены Толщина стены должна быть больше ноля\n";
            }
            if (h0 <= 0)
                errorMessage += "Параметр h0 меньше или равен нулю\n";
            if (parameters == null)
                errorMessage += "Не созданы параметры подрезки контура\n";            
            if (String.IsNullOrEmpty(errorMessage))
                return Result.Success();
            else
                return Result.Failure(errorMessage);
        }

        public virtual Result<GeometryContour> CreateContour(double offset)
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
                    return Result.Failure<GeometryContour>($"Не удалорсь добавить сторону {(ContourSideName)values.GetValue(i)}");
            }

            return contour;

        }
    }
}
