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
        public List<ContourSideName> ActiveSides { get; }
        public Dictionary<string, double> DoubleParameters { get; set; }
        public Dictionary<string, int> IntParameters { get; set; }
        

        public PunchingContourParameters(
            PunchingContourType type,
            Dictionary<string, double> doubleParameters,
            Dictionary<string, int> intParameters)
        {
            Type = type;
            DoubleParameters = doubleParameters;
            IntParameters = intParameters;
            ActiveSides = new List<ContourSideName>();
            switch (type)
            {
                case PunchingContourType.EndWall:
                    ActiveSides.Add(ContourSideName.Left);
                    ActiveSides.Add(ContourSideName.Bottom);
                    ActiveSides.Add(ContourSideName.Right);
                    break;
                case PunchingContourType.WallCorner:
                    ActiveSides.Add(ContourSideName.Bottom);
                    ActiveSides.Add(ContourSideName.Right);
                    break;
                case PunchingContourType.Pylon:
                    ActiveSides.Add(ContourSideName.Top);
                    ActiveSides.Add(ContourSideName.Left);
                    ActiveSides.Add(ContourSideName.Bottom);
                    ActiveSides.Add(ContourSideName.Right);
                    break;
            }
        }
    }
}
