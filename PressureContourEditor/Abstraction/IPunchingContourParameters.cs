using PressureContourEditor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Abstraction
{
    public interface IPunchingContourParameters
    {
        PunchingContourType Type { get; set; }
        HashSet<ContourSideName> ActiveSides { get; set; }
        Dictionary<DimensionsRole, double> Dimensions { get; set; }
        Dictionary<DoubleParametersRole, double> DoubleParameters { get; set; }
        Dictionary<IntParametersRole, int> IntParameters { get; set; }
        GeometryContour ContourHalfH0 { get; }
        GeometryContour Contour6H0 { get; }
        bool IsNotNullOrEmptyParameters(out string errorMessage);
    }
}

