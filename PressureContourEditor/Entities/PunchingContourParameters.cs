using PressureContourEditor.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Entities
{
    public class PunchingContourParameters
    {
        private readonly ICreateContourService _createContourService;
        public PunchingContourType Type { get; set; }
        public HashSet<ContourSideName> ActiveSides { get; set; }
        public Dictionary<DimensionsRole,double> Dimensions { get; set; }
        public Dictionary<DoubleParametersRole, double> DoubleParameters { get; set; }
        public Dictionary<IntParametersRole, int> IntParameters { get; set; }

        public GeometryContour ContourHalfH0 { get; }
        public GeometryContour Contour6H0 { get; }

        /// <summary>
        /// Создание экземпляра зоны приложения продавливающего усилия для расчета на продавливания
        /// </summary>
        /// <param name="createContourService">Сервис для создания геометрического контура</param>
        /// <param name="type">Тип расчета: край стены, угол стен или пилон</param>
        /// <param name="activeSides">в зависимости от типа расчета какие стороны учитываются</param>
        /// <param name="dimensions">размеры зона продавливания</param>
        /// <param name="doubleParameters">характеристики для расчета</param>
        /// <param name="intParameters">для активации в ревите разрешения для редакторования - перенести в инфраструктурный слой</param>

        public PunchingContourParameters(
            ICreateContourService createContourService,
            PunchingContourType type,
            HashSet<ContourSideName> activeSides,
            Dictionary<DimensionsRole,double> dimensions,
            Dictionary<DoubleParametersRole, double> doubleParameters,
            Dictionary<IntParametersRole, int> intParameters)
        {
            _createContourService = createContourService;
            Type = type;
            ActiveSides = activeSides;
            Dimensions = dimensions;
            DoubleParameters = doubleParameters;
            IntParameters = intParameters;

            double h0 = DoubleParameters[DoubleParametersRole.H0];

            ContourHalfH0 = _createContourService.CreateContour(this, 0.5 * h0);
            Contour6H0 = _createContourService.CreateContour(this, 6 * h0);
        }



        public bool IsNotNullOrEmptyParameters(out string errorMessage)
        {
            errorMessage = string.Empty;
            if (ActiveSides == null)
                errorMessage += "ActiveSides is null";
            if (ActiveSides != null && ActiveSides.Count == 0)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "ActiveSides is empty";
            }
            if (Dimensions == null)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "Dimensions is null";
            }
            if (Dimensions != null && Dimensions.Count == 0)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "Dimensions is empty";
            }
            if (DoubleParameters == null)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "DoubleParameters is null";
            }
            if (DoubleParameters != null && DoubleParameters.Count == 0)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "DoubleParameters is empty";
            }
            if (IntParameters == null)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "IntParameters is null";
            }
            if (IntParameters != null && IntParameters.Count == 0)
            {
                errorMessage = AddReturn(errorMessage);
                errorMessage += "IntParameters is empty";
            }
            return String.IsNullOrEmpty(errorMessage);
                
        }

        private string AddReturn(string message)
        {
            message += !String.IsNullOrEmpty(message) ? "/n" : string.Empty;
            return message;
        }
    }
}
