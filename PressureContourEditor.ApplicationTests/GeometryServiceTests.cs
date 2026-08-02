using Moq;
using PressureContourEditor.Application.Abstraction;
using PressureContourEditor.Domain.Abstraction;
using PressureContourEditor.Domain.Entities;
using PressureContourEditor.Domain.GeometryPrimitives;
using PressureContourEditor.Domain.Services;
using PressureContourEditor.Domain.Configuration;

namespace PressureContourEditor.ApplicationTests
{
    public class GeometryServiceTests
    {
        private IParameterNameConfig _parameterNameConfig;
        private IGeometryService _service;
        private GeometryContour _contour;
        private IntersectionLines _lines;
        [SetUp]
        public void Setup()
        {
            var mockConfig = new Mock<IParameterNameConfig>();          

            _service = new GeometryService(mockConfig.Object);
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

        public void CalculateParameter_ValidParameters_HoleOnBottom()
        {
            // Arrange
            moc
            // Act

            // Assert

        }
    }
}