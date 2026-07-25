using PressureContourEditor.Domain.Entities;
using PressureContourEditor.Domain.GeometryPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Services
{
    public class CreateContourService
    {
        public GeometryContour CreateContour(PunchingContourParameters punchingContour, double offset)
        {
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
                    bottomLeft = new Point2D(thickness+thicness2-thickness/2 - offset, -offset);
                    topRight = new Point2D(thickness / 2 + offset, thickness + thicness2);
                    topLeft = new Point2D(thickness + thicness2 - thickness / 2 - offset, thickness + thicness2);
                    center = new Point2D(0, (thickness + 0.5 * h0) * 0.5);
                    break;
            }

        }
    }
}
