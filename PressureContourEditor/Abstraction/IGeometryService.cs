using CSharpFunctionalExtensions;
using PressureContourEditor.Domain.Configuration;
using PressureContourEditor.Domain.Entities;
using PressureContourEditor.Domain.GeometryPrimitives;

namespace PressureContourEditor.Domain.Abstraction
{
    public interface IGeometryService
    {
        public Result<List<IntersectionPoint>> LineWithContourIntersection(IntersectionLines lines, GeometryContour contour);

        public Result<Dictionary<(ContourSideName, PressureContourParametersRole), double>> CalculateParameters(
            GeometryContour contourHalfH0,
            List<IntersectionPoint> intersectionPoints);
    }
}
