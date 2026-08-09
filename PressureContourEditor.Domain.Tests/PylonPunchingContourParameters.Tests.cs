using System;
using System.Collections.Generic;
using NUnit.Framework;
using PressureContourEditor.Domain.Abstraction;
using PressureContourEditor.Domain.Entities;
using Moq;

namespace PressureContourEditor.Tests.Domain.Entities
{
    [TestFixture]
    public class PylonPunchingContourParametersTests
    {
        private Mock<ICreateContourService> _moqCreateContourService;
        private GeometryContour _moqContour;
        private PunchingContour _punchingContour;

        private const double H0 = 160.0;
        private const double Thickness = 400.0;
        private const double Thickness2 = 250.0;
        private const double PylonLength = 800.0;
        private const double OffsetFromStart = 50.0;
        private const PunchingContourType DefaultType = PunchingContourType.Pylon;


        [SetUp]
        public void Setup()
        {
            _moqCreateContourService = new Mock<ICreateContourService>();
            _moqContour = new GeometryContour();

            _moqCreateContourService
                .Setup(x => x.CreateContour(It.IsAny<PunchingContour>(), It.IsAny<double>()))
                .Returns(_moqContour);

            var activeSides = CreateDafaultActiveSidesForPylon();
            var dimensions = CreateDefaultDimensionsForPylon();
            var parameters = CreateDefaultParameters();

            _punchingContour = new PunchingContour(
                _moqCreateContourService.Object,
                DefaultType,
                activeSides,
                dimensions,
                H0,
                parameters);
        }
        // Helper method for create testing data
        // Pylon
        private static HashSet<ContourSideName> CreateDafaultActiveSidesForPylon() =>
            new HashSet<ContourSideName>
            {
                ContourSideName.Top,
                ContourSideName.Left,
                ContourSideName.Bottom,
                ContourSideName.Right
            };

        private static Dictionary<DimensionsRole, double> CreateDefaultDimensionsForPylon() =>
            new Dictionary<DimensionsRole, double>
            {
                [DimensionsRole.Thickness] = Thickness,
                [DimensionsRole.PylonLength] = PylonLength
            };

        // EndWall
        private static HashSet<ContourSideName> CreateDafaultActiveSidesForEndWall() =>
            new HashSet<ContourSideName>()
            {
                ContourSideName.Left,
                ContourSideName.Bottom,
                ContourSideName.Right
            };
        private static Dictionary<DimensionsRole, double> CreateDefaultDimensionsForEndWall() =>
            new Dictionary<DimensionsRole, double>
            {
                [DimensionsRole.Thickness] = Thickness
            };

        // WallCorner
        private static HashSet<ContourSideName> CreateDafaultActiveSidesForWallCorner() =>
            new HashSet<ContourSideName>()
            {
                ContourSideName.Bottom,
                ContourSideName.Right
            };
        private static Dictionary<DimensionsRole, double> CreateDefaultDimensionsForWallCorner() =>
            new Dictionary<DimensionsRole, double>
            {
                [DimensionsRole.Thickness] = Thickness,
                [DimensionsRole.Thickness2] = Thickness2
            };

        // Parameters
        private static Dictionary<(ContourSideName, PressureContourParametersRole), double> CreateDefaultParameters() =>
            new Dictionary<(ContourSideName, PressureContourParametersRole), double>
            {
                // Top
                [(ContourSideName.Top, PressureContourParametersRole.OffsetFromStart)] = 0.0,
                [(ContourSideName.Top, PressureContourParametersRole.OffsetFromEnd)] = 0.0,
                [(ContourSideName.Top, PressureContourParametersRole.HoleOffsetFromStart)] = 0.0,
                [(ContourSideName.Top, PressureContourParametersRole.HoleWidth)] = 0.0,
                // Left
                [(ContourSideName.Left, PressureContourParametersRole.OffsetFromStart)] = 0.0,
                [(ContourSideName.Left, PressureContourParametersRole.OffsetFromEnd)] = 0.0,
                [(ContourSideName.Left, PressureContourParametersRole.HoleOffsetFromStart)] = 0.0,
                [(ContourSideName.Left, PressureContourParametersRole.HoleWidth)] = 0.0,
                // Bottom
                [(ContourSideName.Bottom, PressureContourParametersRole.OffsetFromStart)] = 0.0,
                [(ContourSideName.Bottom, PressureContourParametersRole.OffsetFromEnd)] = 0.0,
                [(ContourSideName.Bottom, PressureContourParametersRole.HoleOffsetFromStart)] = 0.0,
                [(ContourSideName.Bottom, PressureContourParametersRole.HoleWidth)] = 0.0,
                // Right
                [(ContourSideName.Right, PressureContourParametersRole.OffsetFromStart)] = 0.0,
                [(ContourSideName.Right, PressureContourParametersRole.OffsetFromEnd)] = 0.0,
                [(ContourSideName.Right, PressureContourParametersRole.HoleOffsetFromStart)] = 0.0,
                [(ContourSideName.Right, PressureContourParametersRole.HoleWidth)] = 0.0
            };

        private static PunchingContour CreatePunchingContour(
            Mock<ICreateContourService> moqCreateContourService = null,
            PunchingContourType? type = null,
            HashSet<ContourSideName> activeSides = null,
            Dictionary<DimensionsRole, double> dimensions = null,
            double? h0 = null,
            Dictionary<(ContourSideName, PressureContourParametersRole), double> parameters = null)
        {
            return new PunchingContour(
                moqCreateContourService?.Object ?? new Mock<ICreateContourService>().Object,
                type ?? DefaultType,
                activeSides ?? CreateDafaultActiveSidesForPylon(),
                dimensions ?? CreateDefaultDimensionsForPylon(),
                h0 ?? H0,
                parameters ?? CreateDefaultParameters());
        }
        [Test]
        public void Ctor_ShouldCreateInstance_WhenAllParametersAreValid()
        {
            // Arrange
            var expectedType = PunchingContourType.Pylon;
            var expectedActiveSides = CreateDafaultActiveSidesForPylon();
            var expectedDimensions = CreateDefaultDimensionsForPylon();
            var expectedParameters = CreateDefaultParameters();

            // Act
            var result = new PunchingContour(
                _moqCreateContourService.Object,
                expectedType,
                expectedActiveSides,
                expectedDimensions,
                H0,
                expectedParameters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Type, Is.EqualTo(expectedType));
                Assert.That(result.ActiveSides, Is.EqualTo(expectedActiveSides));
                Assert.That(result.Dimensions, Is.EqualTo(expectedDimensions));
                Assert.That(result.H0, Is.EqualTo(H0));
                Assert.That(result.Parameters, Is.EqualTo(expectedParameters));
                Assert.That(result.ContourHalfH0.ErrorMessage, Is.Empty);
                Assert.That(result.Contour6H0.ErrorMessage, Is.Empty);
            });
        }
        [Test]
        public void Ctor_WhenParametersAreNull_ShouldThrowArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                Assert.That(
                    () => new PunchingContour(
                        null!,
                        DefaultType,
                        CreateDafaultActiveSidesForPylon(),
                        CreateDefaultDimensionsForPylon(),
                        H0,
                        CreateDefaultParameters()),
                    Throws.TypeOf<ArgumentNullException>()
                    .With.Message.Contains("createContourService"));
                Assert.That(
                    () => new PunchingContour(
                        _moqCreateContourService.Object,
                        DefaultType,
                        null!,
                        CreateDefaultDimensionsForPylon(),
                        H0,
                        CreateDefaultParameters()),
                    Throws.TypeOf<ArgumentNullException>()
                    .With.Message.Contains("activeSides"));
                Assert.That(
                    () => new PunchingContour(
                        _moqCreateContourService.Object,
                        DefaultType,
                        CreateDafaultActiveSidesForPylon(),
                        null!,
                        H0,
                        CreateDefaultParameters()),
                    Throws.TypeOf<ArgumentNullException>()
                    .With.Message.Contains("dimensions"));
                Assert.That(
                    () => new PunchingContour(
                        _moqCreateContourService.Object,
                        DefaultType,
                        CreateDafaultActiveSidesForPylon(),
                        CreateDefaultDimensionsForPylon(),
                        H0,
                        null!),
                    Throws.TypeOf<ArgumentNullException>()
                    .With.Message.Contains("parameters"));
            });
        }




        [Test]
        public void IsNotNullOrEmptyParameters_WhenParametersAreNull_ErrorMessage()
        {
            // Arrange
            _punchingContour.ActiveSides.Clear();

            // Act
            var result = _punchingContour.IsNotNullOrEmptyParameters(out string errorMessage);

            // Assert
            Assert.AreEqual("ActiveSides is empty", errorMessage);
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
            Assert.AreEqual("Dimensions is empty", errorMessage);
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