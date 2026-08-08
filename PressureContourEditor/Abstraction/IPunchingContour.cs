using PressureContourEditor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Abstraction
{
    public interface IPunchingContour
    {
        PunchingContourType Type { get; set; }
        HashSet<ContourSideName> ActiveSides { get; set; }
        Dictionary<DimensionsRole, double> Dimensions { get; set; }
        double H0 { get; }
        Dictionary<(ContourSideName, PressureContourParametersRole), double> Parameters { get; set; }
        GeometryContour ContourHalfH0 { get; }
        GeometryContour Contour6H0 { get; }
        bool IsNotNullOrEmptyParameters(out string errorMessage);
    }
}

