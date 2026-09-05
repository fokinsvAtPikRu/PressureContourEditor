using PressureContourEditor.Domain.Entities;
using PressureContourEditor.Domain.GeometryPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Abstraction
{
    public interface IRevitRepository
    {
        Task<PressureContour> GetSelectedInstanceAsync();
        Task<Point2D> GetSelectedPointAsync();
        Task UpdateIstanceParametersAsync(PressureContour pressureContour);
    }
}
