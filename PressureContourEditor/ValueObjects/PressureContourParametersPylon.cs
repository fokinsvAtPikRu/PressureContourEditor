using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.ValueObjects
{
    public class PressureContourParametersPylon : PressureContourParameters
    {
        public PressureContourParametersPylon() : base()
        {
            BuildParameters();
        }
    }
}
