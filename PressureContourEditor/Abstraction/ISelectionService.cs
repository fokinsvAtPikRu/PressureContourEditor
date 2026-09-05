using PressureContourEditor.Domain.Entities;
using PressureContourEditor.Domain.GeometryPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Abstraction
{
    public interface ISelectionService
    {
        public Task<PressureContour> PickObject(string prompt = "Выберите семейство"); 
        public Task<Point2D> PickPoint(string prompt = "Выберите точку"); 
    }
}
