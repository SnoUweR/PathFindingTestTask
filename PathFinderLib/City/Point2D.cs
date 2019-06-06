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
        /// Конструктор точки.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        public Point2D(float x, float y)
        {
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
            return (float) Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
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
        /// Выводит имя объекта с точкой в виде [X;Y].
        /// </summary>
        /// <returns>Строкое представление объекта в виде [X;Y].</returns>
        public override string ToString()
        {
            return $"[{X};{Y}]";
        }
    }
}