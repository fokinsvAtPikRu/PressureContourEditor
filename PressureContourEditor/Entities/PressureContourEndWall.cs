using CSharpFunctionalExtensions;
using PressureContourEditor.Domain.Exceptions;
using PressureContourEditor.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Entities
{
    public class PressureContourEndWall : PressureContour
    {        
        private PressureContourEndWall(
            PressureContourType pressureContourType,
            Dictionary<DimensionsRole, double> dimensions,
            double h0,
            Dictionary<ParameterKey, double> parameters) 
            : base(
             pressureContourType,dimensions,h0,parameters)
        {
            Type=PressureContourType.EndWall;
            ActiveSides = new HashSet<ContourSideName> { ContourSideName.Left, ContourSideName.Bottom, ContourSideName.Right };
            Dimensions = dimensions;
            H0 = h0;
            Parameters = new PressureContourParametersEndWall();
        }
        
        public override Result Validate(
            PressureContourType pressureContourType,
            Dictionary<DimensionsRole, double> dimensions,
            double h0,
            Dictionary<ParameterKey, double> parameters)
        {
            var result = base.Validate(pressureContourType, dimensions, h0, parameters);
            string errorMessage = String.Empty;
            if (result.IsFailure)
                errorMessage = result.Error;
            if (pressureContourType != PressureContourType.Pylon)
                errorMessage += "Неверный тип контура\n";
            if (parameters.Count != 15)
                errorMessage += "Неверное количество параметров для сторон\n";
            if (String.IsNullOrEmpty(errorMessage))
                return Result.Success();
            else
                return Result.Failure(errorMessage);
        }
    }
}
