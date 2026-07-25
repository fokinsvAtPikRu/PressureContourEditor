using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Entities
{
    public enum DoubleParametersRole
    {
        H0,
        LeftSideOffsetFromBottom,
        LeftSideOffsetFromTop,        
        LeftSideHoleWidth,
        LeftSideHoleOffsetFromTop,
        BottomSideOffsetFromLeft,
        BottomSideOffsetFromRight,
        BottomSideHoleWidth,
        BottomSideHoleOffsetFromLeft,
        RightSideOffsetFromBottom,
        RightSideOffsetFromTop,
        RightSideHoleWidth,
        RightSideHoleOffsetFromTop,
        TopSideOffsetFromLeft,
        TopSideOffsetFromRight,
        TopSideHoleWidth,
        TopSideHoleOffsetFromLeft
    }
    public enum IntParametersRole
    {
        EditContourEnabled,
        LeftSideIsOn,
        BottomSideIsOn,
        RightSideIsOn,
        TopSideIsOn
    }
}
