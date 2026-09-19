using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Exceptions
{
    public class CreateContourException : DomainException
    {
        public CreateContourException(string message)
            : base(message) { }
    }
}
