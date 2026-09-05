using PressureContourEditor.Domain.Abstraction;
using PressureContourEditor.Domain.Entities;
using PressureContourEditor.Domain.GeometryPrimitives;
using PressureContourEditor.Infrastacture.Context;
using System;
using System.Threading.Tasks;

namespace PressureContourEditor.Infrastacture.Repositories
{
    public class RevitRepository : IRevitRepository
    {
        private readonly RevitContext _context;
        private readonly ISelectionService _selectionService;

        public RevitRepository(
            RevitContext context,
            ISelectionService selectionService)
        {
            _context = context;
            _selectionService = selectionService;
        }

        public Task<PressureContour> GetSelectedInstanceAsync()
        {
            var instance = _selectionService.PickObject();

        }

        public Task<Point2D> GetSelectedPointAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateIstanceParametersAsync(PressureContour pressureContour)
        {
            throw new NotImplementedException();
        }
    }
}
