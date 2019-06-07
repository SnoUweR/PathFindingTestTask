using NUnit.Framework;
using PathFinderLib.City;

namespace PathFinderTests
{
    [TestFixture]
    public class Point2DTests
    {
        [Test]
        public void TestIsBetweenTwoPointsOnSameLine()
        {
            Point2D firstPoint = new Point2D(0, 0);
            Point2D secondPoint = new Point2D(5, 0);

            Point2D thirdPoint = new Point2D(2, 0);
            Assert.IsTrue(thirdPoint.IsBetweenTwoPointsOnSameLine(firstPoint, secondPoint));

            Assert.IsTrue(firstPoint.IsBetweenTwoPointsOnSameLine(firstPoint, secondPoint));
            Assert.IsTrue(secondPoint.IsBetweenTwoPointsOnSameLine(firstPoint, secondPoint));

            Point2D fourthPoint = new Point2D(6, 0);
            Assert.IsFalse(fourthPoint.IsBetweenTwoPointsOnSameLine(firstPoint, secondPoint));
            
            // Диагональ
            firstPoint = new Point2D(-10, -10);
            secondPoint = new Point2D(10, 10);
            thirdPoint = new Point2D(0, 0);
            Assert.IsTrue(thirdPoint.IsBetweenTwoPointsOnSameLine(firstPoint, secondPoint));  
            
            // Проверка граничных точек.
            firstPoint = new Point2D(Point2D.MinValue, Point2D.MinValue);
            secondPoint = new Point2D(Point2D.MaxValue, Point2D.MinValue);
            thirdPoint = new Point2D(Point2D.MaxValue / 2, Point2D.MinValue);
            Assert.IsTrue(thirdPoint.IsBetweenTwoPointsOnSameLine(firstPoint, secondPoint));
        }
    }
}