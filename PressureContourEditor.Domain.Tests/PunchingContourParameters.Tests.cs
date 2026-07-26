using System;
using System.Collections.Generic;
using NUnit.Framework;
using PressureContourEditor.Domain.Entities;

namespace PressureContourEditor.Tests.Domain.Entities
{
    
    public class PunchingContourParametersTests
    {
        private PunchingContourParameters _punchingContour;        
        [SetUp]
        public void Setup()
        {
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
                type,
                activeSides,
                dimensions,
                doubleParameters,
                intParameters);
        }

        [Test]
        public void PunchingContour_ActiveSideIsEmpty_ErrorMessage()
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
        public void PunchingContour_DimensionsIsEmpty_ErrorMessage()
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
        public void PunchingContour_DoubleParameterIsEmpty_ErrorMessage()
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
        public void PunchingContour_IntParameterIsEmpty_ErrorMessage()
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
        public void PunchingContour_CorrectParameters_ResultIsTrue()
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