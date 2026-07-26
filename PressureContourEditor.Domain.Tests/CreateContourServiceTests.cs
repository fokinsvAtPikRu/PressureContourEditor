using CSharpFunctionalExtensions;
using PressureContourEditor.Domain.Abstraction;
using PressureContourEditor.Domain.Entities;
using PressureContourEditor.Domain.GeometryPrimitives;
using PressureContourEditor.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureContourEditor.Domain.Tests
{
    public class CreateContourServiceTests
    {
        private ICreateContourService _createContourService;
        private PunchingContourParameters _punchingContour;

        [SetUp]
        public void Setup()
        {
            _createContourService = new CreateContourService();

            PunchingContourType type = PunchingContourType.Pylon;

            HashSet<ContourSideName> activeSides = new HashSet<ContourSideName>();
            activeSides.Add(ContourSideName.Right);
            activeSides.Add(ContourSideName.Left);
            activeSides.Add(ContourSideName.Top);
            activeSides.Add(ContourSideName.Bottom);

            Dictionary<DimensionsRole, double> dimensions = new Dictionary<DimensionsRole, double>();
            dimensions.Add(DimensionsRole.Thickness, 400.0);
            dimensions.Add(DimensionsRole.PylonLength, 800.0);

            Dictionary<DoubleParametersRole, double> doubleParameters = new Dictionary<DoubleParametersRole, double>();
            doubleParameters.Add(DoubleParametersRole.H0, 160.0);

            Dictionary<IntParametersRole, int> intParameters = new Dictionary<IntParametersRole, int>();
            intParameters.Add(IntParametersRole.EditContourEnabled, 1);

            _punchingContour = new PunchingContourParameters(
                type,
                activeSides,
                dimensions,
                doubleParameters,
                intParameters);
        }

        [Test]
        public void CreateContour_Pylon_Result()
        {
            // Arrange
            double offset = 0.0;

            // Act
            var result = _createContourService.CreateContour(_punchingContour, offset);
            
            Point2D topRight = result.Value.Lines[ContourSideName.Top].StartPoint;
            Point2D topLeft = result.Value.Lines[ContourSideName.Top].EndPoint;
            Point2D bottomLeft = result.Value.Lines[ContourSideName.Bottom].StartPoint;
            Point2D bottomRight = result.Value.Lines[ContourSideName.Bottom].EndPoint;

            Point2D topRightExpected = new Point2D(200, 400);
            Point2D topLeftExpected = new Point2D(-200, 400);
            Point2D bottomLeftExpected = new Point2D(-200, -400);
            Point2D bottomRightExpected = new Point2D(-200, 400);

            // Assert            
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(topRightExpected, topRight,0.0000001);
            Assert.AreEqual(topLeftExpected, topLeft, 0.0000001);
            Assert.AreEqual(bottomLeftExpected, bottomRight, 0.0000001);
            Assert.AreEqual(bottomRightExpected, bottomRight, 0.0000001);
        }

    }
}
