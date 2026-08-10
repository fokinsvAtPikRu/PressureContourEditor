using PressureContourEditor.Domain.Entities;
using PressureContourEditor.Domain.GeometryPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Application.Abstraction
{
    public interface IRevitPressureContour
    {
        public string Id { get; }
        public Point2D LocationPoint { get; }
        public double Rotation { get; }
        public bool IsMirrored { get; }
        public GeometryContour CalculateCoordinate(GeometryContour contour);
    }
}
