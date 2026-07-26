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
        public HashSet<ContourSideName> ActiveSides { get; set; }
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
            Dimensions = dimensions;
            DoubleParameters = doubleParameters;
            IntParameters = intParameters;            
        }

        public bool IsNotNullOrEmptyParameters(out string errorMessage)
        {
            errorMessage = string.Empty;
            if (ActiveSides == null)
                errorMessage += "ActiveSides is null";
            if (ActiveSides != null && ActiveSides.Count == 0)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "ActiveSides is empty";
            }
            if (Dimensions == null)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "Dimensions is null";
            }
            if (Dimensions != null && Dimensions.Count == 0)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "Dimensions is empty";
            }
            if (DoubleParameters == null)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "DoubleParameters is null";
            }
            if (DoubleParameters != null && DoubleParameters.Count == 0)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "DoubleParameters is empty";
            }
            if (IntParameters == null)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "IntParameters is null";
            }
            if (IntParameters != null && IntParameters.Count == 0)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "IntParameters is empty";
            }
            return String.IsNullOrEmpty(errorMessage);
                
        }

        private string AddReturn(string message)
        {
            message += !String.IsNullOrEmpty(message) ? "/n" : string.Empty;
            return message;
        }
    }
}
