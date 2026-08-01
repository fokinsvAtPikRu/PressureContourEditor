using System;
using System.Collections.Generic;
using NUnit.Framework;
using PressureContourEditor.Domain.Abstraction;
using PressureContourEditor.Domain.Entities;
using Moq;

namespace PressureContourEditor.Tests.Domain.Entities
{
    [TestFixture]
    public class PunchingContourParametersTests
    {
        private Mock<ICreateContourService> _moqCreateContourService;
        private GeometryContour _moqContour;
        private PunchingContourParameters _punchingContour;
        
        [SetUp]
        public void Setup()
        {
            _moqCreateContourService = new Mock<ICreateContourService>();
            _moqContour = new GeometryContour();

            _moqCreateContourService
                .Setup(x => x.CreateContour(It.IsAny<PunchingContourParameters>(), It.IsAny<double>()))
                .Returns(_moqContour);

            PunchingContourType type = PunchingContourType.Pylon;

            HashSet< ContourSideName> activeSides = new HashSet< ContourSideName>();
            activeSides.Add(ContourSideName.Right);
            activeSides.Add(ContourSideName.Left);
            activeSides.Add(ContourSideName.Top);
            activeSides.Add(ContourSideName.Bottom);

            Dictionary<DimensionsRole, double> dimensions = new Dictionary<DimensionsRole, double>();
            dimensions.Add(DimensionsRole.Thickness, 400.0);
            dimensions.Add(DimensionsRole.PylonLength, 800.0);

            Dictionary< DoubleParametersRole, double> doubleParameters = new Dictionary<DoubleParametersRole, double>();
            doubleParameters.Add(DoubleParametersRole.H0, 160.0);

            Dictionary<IntParametersRole, int> intParameters =new Dictionary<IntParametersRole, int>();
            intParameters.Add(IntParametersRole.EditContourEnabled, 1);

            _punchingContour =new PunchingContourParameters(
                _moqCreateContourService.Object,                
                type,
                activeSides,
                dimensions,
                doubleParameters,
                intParameters);
        }
        [Test]
        public void Ctor_ShouldCreateInstance_WhenAllParametersAreValid()
        {
            // Arrange
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

            // Act
            var result = new PunchingContourParameters(
                _moqCreateContourService.Object,
                type,
                activeSides,
                dimensions,
                doubleParameters,
                intParameters);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(type, result.Type);
            Assert.AreEqual(activeSides, result.ActiveSides);
            Assert.AreEqual(dimensions, result.Dimensions);
            Assert.AreEqual(doubleParameters, result.DoubleParameters);
            Assert.AreEqual(intParameters, result.IntParameters);
            Assert.AreEqual(String.Empty, result.ContourHalfH0.ErrorMessage);
            Assert.AreEqual(String.Empty, result.Contour6H0.ErrorMessage);
        }


        [Test]
        public void IsNotNullOrEmptyParameters_ActiveSideIsEmpty_ErrorMessage()
        {
            // Arrange
            _punchingContour.ActiveSides.Clear();

            // Act
            var result = _punchingContour.IsNotNullOrEmptyParameters(out string errorMessage);

            // Assert
            Assert.AreEqual("ActiveSides is empty", errorMessage );
            Assert.IsFalse(result);
        }

        [Test]
        public void IsNotNullOrEmptyParameters_DimensionsIsEmpty_ErrorMessage()
        {
            // Arrange
            _punchingContour.Dimensions.Clear();

            // Act
            var result = _punchingContour.IsNotNullOrEmptyParameters(out string errorMessage);

            // Assert
            Assert.AreEqual("Dimensions is empty", errorMessage );
            Assert.IsFalse(result);
        }

        [Test]
        public void IsNotNullOrEmptyParameters_DoubleParameterIsEmpty_ErrorMessage()
        {
            // Arrange
            _punchingContour.DoubleParameters.Clear();

            // Act
            var result = _punchingContour.IsNotNullOrEmptyParameters(out string errorMessage);

            // Assert
            Assert.AreEqual("DoubleParameters is empty", errorMessage);
            Assert.IsFalse(result);
        }

        [Test]
        public void IsNotNullOrEmptyParameters_IntParameterIsEmpty_ErrorMessage()
        {
            // Arrange
            _punchingContour.IntParameters.Clear();

            // Act
            var result = _punchingContour.IsNotNullOrEmptyParameters(out string errorMessage);

            // Assert
            Assert.AreEqual("IntParameters is empty", errorMessage);
            Assert.IsFalse(result);
        }

        [Test]
        public void IsNotNullOrEmptyParameters_CorrectParameters_ResultIsTrue()
        {
            // Arrange
            

            // Act
            var result = _punchingContour.IsNotNullOrEmptyParameters(out string errorMessage);

            // Assert
            Assert.AreEqual(string.Empty, errorMessage);
            Assert.IsTrue(result);
        }
    }
}