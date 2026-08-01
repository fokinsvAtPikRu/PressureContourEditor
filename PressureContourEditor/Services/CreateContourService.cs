using CSharpFunctionalExtensions;
using PressureContourEditor.Domain.Abstraction;
using PressureContourEditor.Domain.Entities;
using PressureContourEditor.Domain.GeometryPrimitives;
using System.Drawing;

namespace PressureContourEditor.Domain.Services
{
    public class CreateContourService : ICreateContourService
    {
        public GeometryContour CreateContour(PunchingContourParameters punchingContour, double offset)
        {
            if (!punchingContour.IsNotNullOrEmptyParameters(out string errorMessage))
                            
                return new GeometryContour(errorMessage);
            Point2D bottomRight;
            Point2D bottomLeft;
            Point2D topRight;
            Point2D topLeft;
            Point2D center;
            double h0 = punchingContour.DoubleParameters[DoubleParametersRole.H0];
            double thickness = punchingContour.Dimensions[DimensionsRole.Thickness];

            switch (punchingContour.Type)
            {
                case PunchingContourType.EndWall:

                    bottomRight = new Point2D(thickness / 2 + offset, offset);
                    bottomLeft = new Point2D(thickness / 2 - offset, -offset);
                    topRight = new Point2D(thickness / 2 + offset, thickness + 0.5 * h0);
                    topLeft = new Point2D(-thickness / 2 - offset, thickness + 0.5 * h0);
                    center = new Point2D(0, (thickness + 0.5 * h0) * 0.5);
                    break;
                case PunchingContourType.WallCorner:
                    double thicness2 = punchingContour.Dimensions[DimensionsRole.Thickness2];
                    bottomRight = new Point2D(thickness / 2 + offset, offset);
                    bottomLeft = new Point2D(thickness + thicness2 - thickness / 2 - offset, -offset);
                    topRight = new Point2D(thickness / 2 + offset, thickness + thicness2);
                    topLeft = new Point2D(thickness + thicness2 - thickness / 2 - offset, thickness + thicness2);
                    center = new Point2D(0, (thickness + 0.5 * h0) * 0.5);
                    break;
                case PunchingContourType.Pylon:
                    double length = punchingContour.Dimensions[DimensionsRole.PylonLength];
                    bottomRight = new Point2D(thickness / 2 + offset, -length / 2 - offset);
                    bottomLeft = new Point2D(-thickness / 2 - offset, -length / 2 - offset);
                    topRight = new Point2D(thickness / 2 + offset, length / 2 + offset);
                    topLeft = new Point2D(-thickness / 2 - offset, length / 2 + offset);
                    center = new Point2D(0, 0);
                    break;
                default:
                    return new GeometryContour($"{punchingContour.Type} не поддерживается");

            }

            Line2D[] contourLines= new Line2D[4]; 
            contourLines[0] = new Line2D(topRight, topLeft);
            contourLines[1] = new Line2D(topLeft, bottomLeft);
            contourLines[2] = new Line2D(bottomLeft, bottomRight);
            contourLines[3] = new Line2D(bottomRight, topRight);

            var contour = new GeometryContour();
            var values = Enum.GetValues(typeof(ContourSideName));

            for (var i=0;i<contourLines.Length;i++)
            {
                if (!contour.TryAddItem((ContourSideName)values.GetValue(i), contourLines[i]))
                    return new GeometryContour($"Не удалорсь добавить сторону {(ContourSideName)values.GetValue(i)}");
            }

            return contour;

        }
    }
}
