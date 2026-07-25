using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Entities
{
    public class PunchingContourParameters
    {
        private GeometryContour _contourHalfH0;
        private GeometryContour _contour6H0;
        
        public PunchingContourType Type { get; set; }
        public HashSet<ContourSideName> ActiveSides { get; }
        public Dictionary<DimensionsRole,double> Dimensions { get; set; }
        public Dictionary<DoubleParametersRole, double> DoubleParameters { get; set; }
        public Dictionary<IntParametersRole, int> IntParameters { get; set; }
        

        public PunchingContourParameters(
            PunchingContourType type,
            HashSet<ContourSideName> activeSides,
            Dictionary<DimensionsRole,double> dimensions,
            Dictionary<DoubleParametersRole, double> doubleParameters,
            Dictionary<IntParametersRole, int> intParameters)
        {
            Type = type;
            ActiveSides = activeSides;
            DoubleParameters = doubleParameters;
            IntParameters = intParameters;            
        }
    }
}
