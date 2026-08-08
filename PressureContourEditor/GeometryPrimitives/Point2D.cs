using System;
namespace PressureContourEditor.Domain.GeometryPrimitives
{
    public class Point2D
    {
        public double X { get; }
        public double Y { get; }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Point2D other = (Point2D)obj;
            const double epsilon = 1e-9;
            return Math.Abs(X - other.X) < epsilon && Math.Abs(Y - other.Y) < epsilon;
        }

        /// <summary>
        /// Вычисляет евклидово расстояние до другой точки
        /// </summary>
        /// <param name="other">Другая точка</param>
        /// <returns>Расстояние между точками</returns>
        public double DistanceTo(Point2D other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            double dx = X - other.X;
            double dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}