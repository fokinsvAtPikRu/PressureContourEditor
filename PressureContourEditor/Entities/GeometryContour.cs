using PressureContourEditor.Domain.GeometryPrimitives;
using System.Collections;

namespace PressureContourEditor.Domain.Entities
{
    public class GeometryContour : IEnumerable<KeyValuePair<ContourSideName, Line2D>>
    {
        public SortedDictionary<ContourSideName, Line2D> Lines { get; set; }
        public string ErrorMessage { get; set; }

        public GeometryContour()
        {
            Lines = [];
            ErrorMessage = string.Empty;
        }
        public GeometryContour(string errorMessage)
        {
            Lines = [];
            ErrorMessage = errorMessage;
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

        public IEnumerator<KeyValuePair<ContourSideName, Line2D>> GetEnumerator()
        {
            return Lines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
