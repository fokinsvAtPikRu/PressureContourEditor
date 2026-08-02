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
        public Line2D FirstLine { get; }
        public Line2D SecondLine { get; }

        public IntersectionLines(Line2D firstLine, Line2D secondLine) 
        {
            FirstLine = firstLine;
            SecondLine = secondLine;        
        }
    }
}
