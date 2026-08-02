using PressureContourEditor.Domain.GeometryPrimitives;

namespace PressureContourEditor.Domain.Tests
{
    internal class Line2DTests
    {
        private Line2D _firstLine;
        private Line2D _secondLine;
        private Point2D? _point;

        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public void InIntersectWithLine_LineAreParalel_ResultIsFalse()
        {
            // Arrange
            _firstLine = new Line2D(
                new Point2D(-100, 0),
                new Point2D(100, 0));
            _secondLine = new Line2D(
                new Point2D(-100, 100),
                new Point2D(100, 100));
            // Act
            var result = _firstLine.IntersectWithLine(_secondLine, out _point);
            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(_point);
        }
        [Test]
        public void InIntersectWithLine_PointOnLineSegment_IntersectionPoint()
        {
            // Arrange
            _firstLine = new Line2D(
                new Point2D(-100, 0),
                new Point2D(100, 0));
            _secondLine = new Line2D(
                new Point2D(0, 100),
                new Point2D(0, -100));
            // Act
            var result = _firstLine.IntersectWithLine(_secondLine, out _point);
            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(new Point2D(0, 0), _point);
        }
        [Test]
        public void InIntersectWithLine_PointOnLineOutsideSegment_IntersectionPoint()
        {
            // Arrange
            _firstLine = new Line2D(
                new Point2D(-100, 200),
                new Point2D(100, 200));
            _secondLine = new Line2D(
                new Point2D(0, 100),
                new Point2D(0, -100));
            // Act
            var result = _firstLine.IntersectWithLine(_secondLine, out _point);
            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(_point);
        }
    }
}
