using PressureContourEditor.Domain.GeometryPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Entities
{
    public class IntersectionPoint
    {
        public Point2D Point { get; set; }
        public ContourSideName SideName { get; set; }
        public IntersectionPoint(Point2D point, ContourSideName sideName)
        {
            Point = point;
            SideName = sideName;
        }
    }
}
