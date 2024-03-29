using System;

namespace PathFinderLib.City
{
    /// <summary>
    /// Точка с двумя координатами X и Y.
    /// </summary>
    public struct Point2D
    {
        /// <summary>
        /// Координата X.
        /// </summary>
        public float X { get; }
        
        /// <summary>
        /// Координата Y.
        /// </summary>
        public float Y { get; }

        /// <summary>
        /// Минимально возможное значение координаты.
        /// Установлено в int.MinValue по той причине, что с точкой могут выполняться различные преобразования,
        /// такие как умножение координаты на координату, сложение с другим таким умножением и т.п.
        /// </summary>
        public const float MinValue = int.MinValue;
        
        /// <summary>
        /// Максимально возможное значение координаты.
        /// Установлено в int.MaxValue по той причине, что с точкой могут выполняться различные преобразования,
        /// такие как умножение координаты на координату, сложение с другим таким умножением и т.п.
        /// </summary>
        public const float MaxValue = int.MaxValue;
        
        /// <summary>
        /// Конструктор точки.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        public Point2D(float x, float y)
        {
            if (x < MinValue || x > MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(x), 
                    $"Значение координат должно лежать в границах [{MinValue};{MaxValue}]");
            }
            
            if (y < MinValue || y > MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(y), 
                    $"Значение координат должно лежать в границах [{MinValue};{MaxValue}]");
            }

            X = x;
            Y = y;
        }

        /// <summary>
        /// Вычисляет, находится ли геометрически текущая точка между двух указанных точек (в рамках одной линии).
        /// Допускает небольшую погрешность, чтоб скомпенсировать «потери» при работе с float.
        /// Если точка находится ровно на месте <paramref name="a"/> или <paramref name="b"/>, то
        /// также возвращается true.
        /// </summary>
        /// <param name="a">Первая точка.</param>
        /// <param name="b">Вторая точка.</param>
        /// <returns>true, если текущая точка находится между двух указанных, или равна одной из указанных.</returns>
        public bool IsBetweenTwoPointsOnSameLine(Point2D a, Point2D b)
        {
            return Math.Abs(GetPointsDistance(a, this) + GetPointsDistance(b, this) -
                            GetPointsDistance(a, b)) < 0.000001;
        }

        /// <summary>
        /// Вычисляет расстояние между двумя указанными точками.
        /// </summary>
        /// <param name="a">Первая точка.</param>
        /// <param name="b">Вторая точка.</param>
        /// <returns>Расстояние между двумя указанными точками.</returns>
        public static float GetPointsDistance(Point2D a, Point2D b)
        {
            return (float) Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        
        /// <summary>
        /// Перегрузка метода получения хэш значения объекта.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + X.GetHashCode();
                hash = (hash * 7) + Y.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Перегрузка оператора проверки на равенство.
        /// Если координаты сравниваемых точек равны, то значит и их объекты равны.
        /// Не учитывает погрешность при работе с float.
        /// </summary>
        /// <param name="obj">Вторая точка.</param>
        /// <returns>true, если обе точки равны по координатам. false, если нет.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Point2D otherPoint = (Point2D) obj;

            return (X == otherPoint.X) && (Y == otherPoint.Y);
        }

        /// <summary>
        /// Перегрузка оператора сравнения на равенство.
        /// Если координаты сравниваемых точек равны, то значит и их объекты равны.
        /// Не учитывает погрешность при работе с float.
        /// </summary>
        /// <param name="left">Первая точка.</param>
        /// <param name="right">Вторая точка.</param>
        /// <returns>true, если координаты указанных точек равны.</returns>
        public static bool operator ==(Point2D left, Point2D right)
        {
            return left.X == right.X && left.Y == right.Y;
        }
        
        /// <summary>
        /// Перегрузка оператора сравнения на неравенство.
        /// Если какие-то из координат указанных точек не равны, то значит и их объекты не равны.
        /// Не учитывает погрешность при работе с float.
        /// </summary>
        /// <param name="left">Первая точка.</param>
        /// <param name="right">Вторая точка.</param>
        /// <returns>true, если координаты указанных точек не равны.</returns>
        public static bool operator !=(Point2D left, Point2D right)
        {
            return left.X != right.X || left.Y != right.Y;
        }

        /// <summary>
        /// Формирует строку вида [X;Y].
        /// </summary>
        /// <returns>Строкое представление объекта в виде [X;Y].</returns>
        public override string ToString()
        {
            return $"[{X};{Y}]";
        }
    }
}