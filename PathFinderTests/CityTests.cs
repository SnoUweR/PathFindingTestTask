using System;
using System.Collections.Generic;
using NUnit.Framework;
using PathFinderLib.City;
using PathFinderLib.City.Institutions;
using PathFinderLib.City.Institutions.Factories;

namespace PathFinderTests
{
    [TestFixture]
    public class CityTests
    {
        /// <summary>
        /// Функция, проверяющая корректность работы метода нахождения пересечений в двух линиях.
        /// </summary>
        [Test]
        public void TestIntersectPoints()
        {
            Road road1 = new Road(new Point2D(0, 0), new Point2D(0, 10));
            Road road2 = new Road(new Point2D(0, 10), new Point2D(10, 10));

            Point2D intersectPoint;
            
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
        }

        /// <summary>
        /// Функция, проверяющая корректность работы метода постройки учреждений на пересечениях дорог.
        /// </summary>
        [Test]
        public void TestAddRoad()
        {
            City city = DataGenerator.GenerateTestCity();
            
            Dictionary<Point2D, int> validationValues = new Dictionary<Point2D, int>()
            {
                {new Point2D(0, 0), 2},
                {new Point2D(2, 0), 3},
                {new Point2D(5, 0), 3},
                {new Point2D(8, 0), 3},
                {new Point2D(10, 0), 2},
                {new Point2D(0, 4), 3},
                {new Point2D(0, 8), 3},
                {new Point2D(10, 10), 3},
                {new Point2D(0, 11), 2},
                {new Point2D(5, 11), 3},
                {new Point2D(7, 11), 3},
                {new Point2D(10, 11), 2},
            };

            Assert.AreEqual(15, city.Institutions.Count);

            foreach (Institution institution in city.Institutions)
            {
                if (validationValues.ContainsKey(institution.Location))
                {
                    Assert.AreEqual(validationValues[institution.Location], institution.Neighbours.Count, 
                        $"{institution.Location}");
                }
            }
            
            // Проверим город, который пока без дорог. Но так как границы тоже дороги, то должно быть 4 учреждения.
            City city2 = new City(new Point2D(0, 0), new Point2D(10, 10), 
                new RandomBuilder(1337));
            
            Assert.AreEqual(4, city2.Institutions.Count);
            
            Dictionary<Point2D, int> city2ValidationValues = new Dictionary<Point2D, int>()
            {
                {new Point2D(0, 0), 2},
                {new Point2D(10, 0), 2},
                {new Point2D(10, 10), 2},
                {new Point2D(0, 10), 2},
            };
            
            foreach (Institution institution in city2.Institutions)
            {
                if (city2ValidationValues.ContainsKey(institution.Location))
                {
                    Assert.AreEqual(city2ValidationValues[institution.Location], institution.Neighbours.Count, 
                        $"{institution.Location}");
                }
            }
                        
            // Проверяем ситуацию, когда дорога проходит ровно по центру пересечения других двух дорог.
            city2.AddRoad(new Road(new Point2D(-2, -2), new Point2D(14, 14)));

            foreach (Institution institution in city2.Institutions)
            {
                Console.WriteLine(institution.Location);
            }

            // Если дорога прошла между другого пересечения, то новых построек не будет.
            Assert.AreEqual(4, city2.Institutions.Count);
            
            /*
             * Тем не менее, если дорога прошла сквозь другие пересечения, то тогда она должна связать те учреждения,
             * которые до этого не были связаны.
             * То есть, например, те, что по углам располагались. Прямой связи между левым верхним и правым нижним
             * углом не было, а теперь есть. А значит нужно проверить соседей.
             */
            
            Dictionary<Point2D, int> city2ValidationValuesAfterRoad = new Dictionary<Point2D, int>()
            {
                {new Point2D(0, 0), 3},
                {new Point2D(10, 0), 2},
                {new Point2D(10, 10), 3},
                {new Point2D(0, 10), 2},
            };
            
            foreach (Institution institution in city2.Institutions)
            {
                if (city2ValidationValuesAfterRoad.ContainsKey(institution.Location))
                {
                    Assert.AreEqual(city2ValidationValuesAfterRoad[institution.Location], institution.Neighbours.Count, 
                        $"{institution.Location}");
                }
            }
        }
    }
}