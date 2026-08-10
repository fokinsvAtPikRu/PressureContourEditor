using CSharpFunctionalExtensions;
using PressureContourEditor.Domain.GeometryPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Application.Abstraction
{
    public interface IRevitSelectionServices
    {
        Task<Result<Point2D>> PickPointAsync(string prompt);
        Task<Result<IRevitPressureContour>> PickFamilyInstanceAsync(string prompt);
    }
}
