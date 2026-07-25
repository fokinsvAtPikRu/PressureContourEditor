namespace PressureContourEditor.Domain.GeometryPrimitives
{
    public class Line2D
    {
        public Point2D StartPoint { get; set; }
        public Point2D EndPoint { get; set; }
        public Line2D(Point2D startPoint, Point2D endPoint) 
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
        public bool IntersectWithLine(Line2D line, out Point2D? intersectionPoint)
        {
            intersectionPoint = null;

            // Координаты точек
            double x1 = StartPoint.X, y1 = StartPoint.Y;
            double x2 = EndPoint.X, y2 = EndPoint.Y;
            double x3 = line.StartPoint.X, y3 = line.StartPoint.Y;
            double x4 = line.EndPoint.X, y4 = line.EndPoint.Y;

            // Векторы направлений
            double dx1 = x2 - x1;
            double dy1 = y2 - y1;
            double dx2 = x4 - x3;
            double dy2 = y4 - y3;

            // Вычисляем определитель
            double det = dx1 * dy2 - dy1 * dx2;

            // Проверка на параллельность (det = 0)
            if (Math.Abs(det) < 1e-10)
            {
                // Проверяем, совпадают ли линии
                // Проверяем, лежит ли точка line1.StartPoint на line2
                double cross = (x3 - x1) * dy1 - (y3 - y1) * dx1;
                if (Math.Abs(cross) < 1e-10)
                {
                    // Линии совпадают
                    return false;
                }
                else
                {
                    // Линии параллельны, но не совпадают
                    return false;
                }
            }

            // Вычисляем параметры t для первой линии и u для второй
            double t = ((x3 - x1) * dy2 - (y3 - y1) * dx2) / det;
            double u = ((x3 - x1) * dy1 - (y3 - y1) * dx1) / det;

            // Проверяем, лежит ли точка пересечения в пределах отрезков
            bool intersectsSegments = (t >= 0 && t <= 1 && u >= 0 && u <= 1);

            // Вычисляем точку пересечения
            double px = x1 + t * dx1;
            double py = y1 + t * dy1;

            // Округляем для избежания ошибок с плавающей точкой
            px = Math.Round(px, 10);
            py = Math.Round(py, 10);

            intersectionPoint = new Point2D(px, py);

            // Возвращаем 0 в любом случае, если линии не параллельны
            // Так как линии всегда пересекаются в бесконечности
            return true;
        }
    }
}
