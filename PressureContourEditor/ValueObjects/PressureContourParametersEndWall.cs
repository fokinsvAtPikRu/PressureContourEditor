namespace PressureContourEditor.Domain.ValueObjects
{
    public class PressureContourParametersEndWall : PressureContourParameters
    {
        public PressureContourParametersEndWall() : base()
        {
            BuildParameters([ContourSideName.Top]);
        }
    }
}
