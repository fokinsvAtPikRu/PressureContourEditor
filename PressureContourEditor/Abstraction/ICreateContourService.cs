using CSharpFunctionalExtensions;
using PressureContourEditor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Abstraction
{
    public interface ICreateContourService
    {
        public GeometryContour CreateContour(PunchingContourParameters punchingContour, double offset);
    }
}
