using Moq;
using PressureContourEditor.Domain.Abstraction;
using PressureContourEditor.Domain.Entities;
using PressureContourEditor.Domain.GeometryPrimitives;
using PressureContourEditor.Domain.Services;

namespace PressureContourEditor.ApplicationTests
{
    public class GeometryServiceTests
    {
        private IGeometryService _service;
        private GeometryContour _contour;
        private IntersectionLines _lines;
        [SetUp]
        public void Setup()
        {
            _service = new GeometryService();
            _contour = new GeometryContour();
        }

        [Test]
        public void FindIntersectionPoint_ValidData_FindedOneIntersectionPoint()
        {
            // Arrange
            _contour.TryAddItem(ContourSideName.Bottom,
                new Line2D(
                    new Point2D(-100, 0),
                    new Point2D(100, 0)));
            _lines = new IntersectionLines(
                new Line2D(
                    new Point2D(0, 100),
                    new Point2D(0, -100)),
                new Line2D(
                    new Point2D(-200, -200),
                    new Point2D(-200, 200)));
            // Act
            var result = _service.LineWithContourIntersection(_lines, _contour);
            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.Value.Count);
            Assert.AreEqual(
                (new IntersectionPoint(new Point2D(0, 0), ContourSideName.Bottom)).Point,
                result.Value[0].Point);
            Assert.AreEqual(
                (new IntersectionPoint(new Point2D(0, 0), ContourSideName.Bottom)).SideName,
                result.Value[0].SideName);
        }
        [Test]
        public void FindIntersectionPoint_ValidData_FindedTwoIntersectionPoint()
        {
            // Arrange
            _contour.TryAddItem(ContourSideName.Bottom,
                new Line2D(
                    new Point2D(-100, 0),
                    new Point2D(100, 0)));
            _lines = new IntersectionLines(
                new Line2D(
                    new Point2D(0, 100),
                    new Point2D(0, -100)),
                new Line2D(
                    new Point2D(10, 100),
                    new Point2D(10, -100)));
            var expectedFirstPoint = new IntersectionPoint(new Point2D(0, 0), ContourSideName.Bottom);
            var expectedSecondPoint = new IntersectionPoint(new Point2D(10, 0), ContourSideName.Bottom);

            // Act
            var result = _service.LineWithContourIntersection(_lines, _contour);
            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Value.Count);
            // first point
            Assert.AreEqual(expectedFirstPoint.Point, result.Value[0].Point);
            Assert.AreEqual(expectedFirstPoint.SideName, result.Value[0].SideName);
            // second point
            Assert.AreEqual(expectedSecondPoint.Point, result.Value[1].Point);
            Assert.AreEqual(expectedSecondPoint.SideName, result.Value[1].SideName);
        }

        [Test]
        public void CalculateParameter_ValidParameters_HoleOnLeft()
        {
            // Arrange
            Line2D top = new Line2D(new Point2D(100, 100), new Point2D(-100, 100));
            Line2D left = new Line2D(new Point2D(-100, 100), new Point2D(-100, -100));
            Line2D bottom = new Line2D(new Point2D(-100, -100), new Point2D(100, -100));
            Line2D right = new Line2D(new Point2D(100, -100), new Point2D(100, 100));

            _contour = new GeometryContour();
            _contour.TryAddItem(ContourSideName.Top, top);
            _contour.TryAddItem(ContourSideName.Left, left);
            _contour.TryAddItem(ContourSideName.Bottom, bottom);
            _contour.TryAddItem(ContourSideName.Right, right);

            IntersectionPoint point1 = new IntersectionPoint(new Point2D(-100, 0), ContourSideName.Left);
            IntersectionPoint point2 = new IntersectionPoint(new Point2D(-100, 50), ContourSideName.Left);
            List<IntersectionPoint> intersectionPoints = new List<IntersectionPoint>();
            intersectionPoints.Add(point1);
            intersectionPoints.Add(point2);

            var doubleParameters = new Dictionary<DoubleParametersRole, double>
            {
                { DoubleParametersRole.LeftSideHoleWidth, 0.0 },
                { DoubleParametersRole.LeftSideHoleOffsetFromTop, 0.0 }                
            };

            var mockParameters = new Mock<IPunchingContourParameters>();
            mockParameters.Setup(p => p.ContourHalfH0)
                .Returns(_contour);
            mockParameters.Setup(p => p.DoubleParameters)
                .Returns(doubleParameters);            

            // Act
            var result = _service.CalculateParameters(mockParameters.Object, intersectionPoints);
            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(50.0, result.Value[DoubleParametersRole.LeftSideHoleWidth]);
        }
    }
}