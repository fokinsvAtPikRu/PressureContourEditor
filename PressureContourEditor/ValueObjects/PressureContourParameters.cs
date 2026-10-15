using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.ValueObjects
{
    public abstract class PressureContourParameters
    {
        public Dictionary<ParameterKey,double> Parameters { get; set; }

        public PressureContourParameters()
        {
            Parameters = new Dictionary<ParameterKey, double>();
        }

        public Dictionary<ParameterKey, double> BuildParameters(IEnumerable<ContourSideName>? skipSides = null)
        {
            var parameters = new Dictionary<ParameterKey, double>();

            var sides = Enum.GetValues(typeof(ContourSideName));
            var roles = Enum.GetValues(typeof(PressureContourParametersRole));

            for (int i = 0; i < sides.Length; i++)
            {
                var skipSet= new HashSet<ContourSideName>(skipSides ??  Enumerable.Empty<ContourSideName>());
                var side = (ContourSideName)sides.GetValue(i);

                if (skipSet.Contains(side))
                    continue;

                for (int j = 0; j < roles.Length; j++)
                {
                    var role = (PressureContourParametersRole)roles.GetValue(j);
                    var key = new ParameterKey(side, role);
                    parameters[key] = 0.0; 
                }
            }

            return parameters;
        }
    }
}
