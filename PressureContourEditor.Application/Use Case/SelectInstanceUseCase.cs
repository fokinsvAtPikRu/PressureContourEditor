using CSharpFunctionalExtensions;
using PressureContourEditor.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Application.Use_Case
{
    public class SelectInstanceUseCase : ISelectInstanceUseCase
    {
        IRevitSelectionServices _revitSelectionService;

        public SelectInstanceUseCase(IRevitSelectionServices revitSelectionService)
        {
            _revitSelectionService = revitSelectionService;
        }
        public async Task<Result<Pressu>> RunAsync()
        {

            var result = await _revitSelectionService.PickFamilyInstanceAsync("Выберете семейство");
            if (result.IsSuccess)
            {
                return new ResultSelectInstanceUserCase
                {
                    PressureContour = result.Value
                };
            }
            else
            {
                return Result.Failure<ResultSelectInstanceUserCase>(result.Error);
            }
        }
    }
}
