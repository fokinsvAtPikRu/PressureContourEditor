using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Configuration
{
    public class ParameterDictionary
    {
        public Dictionary<string, double> DoubleParameters { get; set; }
        public Dictionary<string, int> IntParameters { get; set; }

    }
}
