using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.ValueObjects
{
    public class PressureContourParametersWallCorner : PressureContourParameters
    {
        public PressureContourParametersWallCorner() : base()
        {
            BuildParameters([ContourSideName.Top, ContourSideName.Left]);
        }
    }
}
