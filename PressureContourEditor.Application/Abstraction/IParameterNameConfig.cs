using PressureContourEditor.Application.DTOs;
using PressureContourEditor.Domain.Configuration;
using PressureContourEditor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Application.Abstraction
{
    public interface IParameterNameConfig
    {
        public string GetParameterName(ParameterRole role, ContourSideName side, bool isStart = false);

        public IReadOnlyCollection<string> GetAllParameterNames();

        public ParameterDictionary GetDefaultValues();

        public SideMappingDto GetSideMapping(ContourSideName side);


        public IReadOnlyList<ContourSideName> GetAvailableSides();


        public bool HasParameter(string parameterName);
    }
}
