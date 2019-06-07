using System;

namespace PathFinderLib.City
{
    /// <summary>
    /// Городская дорога.
    /// </summary>
    public class Road
    {
        /// <summary>
        /// Координаты начала дороги.
        /// </summary>
        private readonly Point2D _beginPoint;

        /// <summary>
        /// Координаты конца дороги.
        /// </summary>
        private readonly Point2D _endPoint;

        /// <summary>
        /// Конструктор объекта дороги.
        /// </summary>
        /// <param name="beginPoint">Координаты начала дороги.</param>
        /// <param name="endPoint">Координаты конца дороги.</param>
        public Road(Point2D beginPoint, Point2D endPoint)
        {
            _beginPoint = beginPoint;
            _endPoint = endPoint;
        }

        /// <summary>
        /// Пытается получить точку пересечения текущей дороги с указанной.
        /// Если точка пересечения есть, то возвращает true, а также устанавливает эту точку в
        /// <paramref name="intersectPoint"/>.
        /// Если точки пересечения нет, то есть дороги не пересекаются, то возвращает false,
        /// а в <paramref name="intersectPoint"/> устанавливает стандартное значение.
        /// Все формулы взяты отсюда: http://mathworld.wolfram.com/Line-LineIntersection.html
        /// </summary>
        /// <param name="anotherRoad">Дорога, с которой нужно проверить пересечение.</param>
        /// <param name="intersectPoint">Точка пересечения двух дорог.</param>
        /// <returns>true, если дороги пересекаются. false, если нет.</returns>
        public bool TryGetIntersectPoint(Road anotherRoad, out Point2D intersectPoint)
        {
            intersectPoint = default(Point2D);
            const float tolerance = 0.000001f;

            float a = Det2(_beginPoint.X - _endPoint.X, _beginPoint.Y - _endPoint.Y,
                anotherRoad._beginPoint.X - anotherRoad._endPoint.X, 
                anotherRoad._beginPoint.Y - anotherRoad._endPoint.Y);

            // Если условие выполняется, то значит прямые параллельны.
            if (Math.Abs(a) < float.Epsilon) return false;

            float d1 = Det2(_beginPoint.X, _beginPoint.Y, _endPoint.X, _endPoint.Y);
            float d2 = Det2(anotherRoad._beginPoint.X, anotherRoad._beginPoint.Y,
                anotherRoad._endPoint.X, anotherRoad._endPoint.Y);
            float x = Det2(d1, _beginPoint.X - _endPoint.X,
                          d2, anotherRoad._beginPoint.X - anotherRoad._endPoint.X) / a;
            float y = Det2(d1, _beginPoint.Y - _endPoint.Y, 
                          d2, anotherRoad._beginPoint.Y - anotherRoad._endPoint.Y) / a;

            if (x < Math.Min(_beginPoint.X, _endPoint.X) - tolerance ||
                x > Math.Max(_beginPoint.X, _endPoint.X) + tolerance)
            {
                return false;
            }
            
            if (y < Math.Min(_beginPoint.Y, _endPoint.Y) - tolerance ||
                y > Math.Max(_beginPoint.Y, _endPoint.Y) + tolerance)
            {
                return false;
            }
            
            if (x < Math.Min(anotherRoad._beginPoint.X, anotherRoad._endPoint.X) - tolerance ||
                x > Math.Max(anotherRoad._beginPoint.X, anotherRoad._endPoint.X) + tolerance)
            {
                return false;
            }

            if (y < Math.Min(anotherRoad._beginPoint.Y, anotherRoad._endPoint.Y) - tolerance ||
                y > Math.Max(anotherRoad._beginPoint.Y, anotherRoad._endPoint.Y) + tolerance)
            {
                return false;
            }

            intersectPoint = new Point2D(x, y);
            return true;
        }

        /// <summary>
        /// Вычисляет и возвращает определитель матрицы 2x2, описанной как {x1, x2, y1, y2}.
        /// </summary>
        /// <returns>Определитель матрицы 2x2.</returns>
        private static float Det2(float x1, float x2, float y1, float y2)
        {
            return (x1 * y2) - (y1 * x2);
        }

        /// <summary>
        /// Возвращает информацию о дороге в виде «НАЧАЛЬНАЯ ТОЧКА->КОНЕЧНАЯ ТОЧКА».
        /// </summary>
        /// <returns>Информация о дороге в виде «НАЧАЛЬНАЯ ТОЧКА->КОНЕЧНАЯ ТОЧКА».</returns>
        public override string ToString()
        {
            return $"{_beginPoint.ToString()}->{_endPoint.ToString()}";
        }
    }
}