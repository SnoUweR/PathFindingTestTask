using PathFinderLib;
using PathFinderLib.City;
using PathFinderLib.City.Institutions.Factories;
using PathFinderLib.GraphEngine;

namespace PathFinderTests
{
    public abstract class DataGenerator
    {
        /// <summary>
        /// Генерирует и возвращает объект с городом, состоящим из
        /// четырех дорог (не считая границы города) и 15 зданиями (пересечениями).
        /// </summary>
        /// <returns>Объект с заполненным дорогами городом.</returns>
        public static City GenerateTestCity(Builder insitutionBuilder)
        {
            City city = new City(new Point2D(0, 0), new Point2D(10, 11), 
                insitutionBuilder);
            city.AddRoad(new Road(new Point2D(2, 0), new Point2D(5, 11)));
            city.AddRoad(new Road(new Point2D(0, 4), new Point2D(5, 0)));
            city.AddRoad(new Road(new Point2D(7, 11), new Point2D(8, 0)));
            city.AddRoad(new Road(new Point2D(0, 8), new Point2D(10, 10)));

            return city;
        }
        
        
        /// <summary>
        /// Генерирует и возвращает объект с городом, состоящим из
        /// четырех дорог (не считая границы города) и 15 зданиями (пересечениями).
        /// </summary>
        /// <returns>Объект с заполненным дорогами городом.</returns>
        public static City GenerateTestCity()
        {
            return GenerateTestCity(new RandomBuilder(1337));
        }

        /// <summary>
        /// Генерирует и возвращает граф, описывающий тестовый город из функции
        /// <see cref="GenerateTestCity"/>.
        /// </summary>
        /// <returns>Граф, описывающий тестовый город.</returns>
        public static Graph GenerateTestCityGraph()
        {
            City testCity = GenerateTestCity();
            return Finder.ConvertCityToGraph(testCity);
        }
    }
}