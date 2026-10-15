using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.ValueObjects
{
    public sealed class ParameterKey : IEquatable<ParameterKey>
    {
        public ContourSideName Side { get; }
        public PressureContourParametersRole Role { get; }
        public ParameterKey(ContourSideName side, PressureContourParametersRole role)
        {
            Side = side;
            Role = role;
        }
        public bool Equals(ParameterKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Side == other.Side && Role == other.Role;
        }
        public override bool Equals(object obj) => Equals(obj as ParameterKey);
        public override int GetHashCode()
        {
            unchecked
            {
                return (int)Side * 397 ^ (int)Role;
            }
        }

        public override string ToString() => $"{Side}.{Role}";
    }    
}
