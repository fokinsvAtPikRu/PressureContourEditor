using PressureContourEditor.Domain.GeometryPrimitives;

namespace PressureContourEditor.Domain.Entities
{
    public class GeometryContour
    {
        public SortedDictionary<ContourSideName,Line2D> Lines { get; set; }

        public GeometryContour() 
        {
            Lines=[];
        }

        public bool TryAddItem(ContourSideName sideName, Line2D line)
        {
            try
            {
                Lines.Add(sideName, line);
            }
            catch (System.ArgumentException)
            {
                return false;
            }
            return true;
        }
    }
}
