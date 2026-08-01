using PressureContourEditor.Domain.GeometryPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Entities
{
    public class IntersectionLines
    {
        private Line2D _firstLine;
        private Line2D _secondLine;

        public IntersectionLines(Line2D firstLine, Line2D secondLine) 
        {
            _firstLine = firstLine;
            _secondLine = secondLine;        
        }
    }
}
