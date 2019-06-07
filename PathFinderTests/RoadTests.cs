using System;
using NUnit.Framework;
using PathFinderLib.City;

namespace PathFinderTests
{
    [TestFixture]
    public class RoadTests
    {
        /// <summary>
        /// Функция, проверяющая корректность работы метода нахождения пересечений в двух линиях.
        /// </summary>
        [Test]
        public void TestTryGetIntersectPoint()
        {
            Point2D intersectPoint;

            Road road1 = new Road(new Point2D(0, 0), new Point2D(0, 10));
            Road road2 = new Road(new Point2D(0, 10), new Point2D(10, 10));


            Assert.True(road1.TryGetIntersectPoint(road2, out intersectPoint));
            Assert.True(intersectPoint == new Point2D(0, 10));

            Road road3 = new Road(new Point2D(5, 15), new Point2D(5, -2));
            
            Assert.False(road1.TryGetIntersectPoint(road3, out _));
            Assert.True(road2.TryGetIntersectPoint(road3, out intersectPoint));
            Assert.True(intersectPoint == new Point2D(5, 10));
            
            // Если пытаемся найти пересечение в прямых, лежащих одинаково, то его быть не должно.
            Road road4 = new Road(new Point2D(0, 0), new Point2D(0, 10));
            Assert.False(road4.TryGetIntersectPoint(road1, out _));
            
            // Пересечение для параллельных прямых тоже должно отсутствовать.
            Road road5 = new Road(new Point2D(1, 0), new Point2D(1, 10));
            Assert.False(road4.TryGetIntersectPoint(road5, out _));
            
            // Граничные условия
            Road road6 = new Road(new Point2D(Point2D.MinValue, Point2D.MinValue),
                new Point2D(Point2D.MaxValue, Point2D.MaxValue));
            
            Road road7 = new Road(new Point2D(Point2D.MaxValue, Point2D.MinValue),
                new Point2D(Point2D.MinValue, Point2D.MaxValue));

            Assert.True(road6.TryGetIntersectPoint(road7, out intersectPoint));
            Assert.AreEqual(new Point2D(0, 0), intersectPoint);
            
            // Имитация перпендикулярно идущих дорог, но которые соприкасаются в граничной точке.
            Road road8 = new Road(new Point2D(Point2D.MinValue, Point2D.MinValue),
                new Point2D(Point2D.MaxValue, Point2D.MinValue));
            Road road9 = new Road(new Point2D(Point2D.MaxValue, Point2D.MinValue),
                new Point2D(Point2D.MaxValue, Point2D.MaxValue));
            
            Assert.True(road8.TryGetIntersectPoint(road9, out intersectPoint));
            Assert.False(float.IsNaN(intersectPoint.X));
            Assert.False(float.IsNaN(intersectPoint.Y));
        }
    }
}