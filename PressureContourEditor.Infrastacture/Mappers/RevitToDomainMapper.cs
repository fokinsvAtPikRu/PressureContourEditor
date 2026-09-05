using Autodesk.Revit.DB;
using PressureContourEditor.Domain.GeometryPrimitives;

namespace PressureContourEditor.Infrastacture.Mappers
{
    public static class RevitToDomainMapper
    {
        public static XYZ ToRevit(this Point2D point) =>
            new XYZ(point.X, point.Y, 0);
        public static Line ToRevit(this Line2D line) =>
            Line.CreateBound(line.StartPoint.ToRevit(), line.EndPoint.ToRevit());
        public static ElementId ToRevitId(this string domainId) =>
            int.TryParse(domainId, out var id) ? new ElementId(id) : ElementId.InvalidElementId;
    }
}
