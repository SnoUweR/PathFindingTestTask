using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PathFinderLib;
using PathFinderLib.City;
using PathFinderLib.City.Institutions;
using PathFinderLib.City.Institutions.Factories;
using PathFinderLib.GraphEngine;
using PathFinderLib.GraphEngine.Algorithms;

namespace PathFinderTests
{
    [TestFixture]
    public class FinderTests
    {
        /// <summary>
        /// Метод, тестирующий функцию конвертации объекта с городом в граф.
        /// </summary>
        [Test]
        public void TestConvertCityToGraph()
        {
            City testCity = DataGenerator.GenerateTestCity();
            Graph graph = Finder.ConvertCityToGraph(testCity);
            
            /**
             * Так как мы создаем тестовый город, в котором изначально уже знаем количество
             * пересечений дорог, то можно проверить количество вершин и ребер.
             */
            
            List<int> validationValues = new List<int>()
            {
                2, 3, 2, 3, 3, 2, 3, 3, 2, 3, 3, 3, 4, 4, 4
            };
            
            Assert.AreEqual(15, graph.Vertices.Count);
            for (int i = 0; i < graph.Vertices.Count; i++)
            {
                Assert.AreEqual(validationValues[i], graph.Vertices[i].Edges.Count);
            }
        }

        [Test]
        public void TestFindPathTo()
        {
            /*
             * Тестовый город дает нам несколько дорог и, соответственно, построек.
             * Тип построек подбирается случайным образом, но так как мы задаем конкретный сид рандома
             * (в функции GenerateTestCityGraph), то первой постройкой (и соответственно вершиной в графе),
             * будет являться госпиталь.
             * 
             * В первую очередь нужно убедиться в том, что выполняется условие «стартовая точка не может
             * оказаться конечной». Для этого пытаемся найти путь до точки того же типа, что и стартовая.
             */
            Graph graph = DataGenerator.GenerateTestCityGraph();
            PathInfo pathInfo = Finder.FindPathTo(graph, graph.Vertices.First(), InstitutionType.Hospital);

            Assert.IsFalse(pathInfo.IsEmptyPath());

            // Проверяем ситуацию, когда весь город состоит из искомых объектов.
            City cityWithSameInstitutes = DataGenerator.GenerateTestCity(new PostOfficeBuilder());
            Graph graphWithSameInstitutes = Finder.ConvertCityToGraph(cityWithSameInstitutes);

            pathInfo = Finder.FindPathTo(graphWithSameInstitutes, 
                graphWithSameInstitutes.Vertices.First(), InstitutionType.PostOffice);
            
            Assert.IsFalse(pathInfo.IsEmptyPath());
            
            // Проверяем ситуацию, когда в городе нет искомого объекта.
            pathInfo = Finder.FindPathTo(graphWithSameInstitutes, 
                graphWithSameInstitutes.Vertices.First(), InstitutionType.Hospital);
            
            Assert.IsTrue(pathInfo.IsEmptyPath());
        }

        [Test]
        public void TestFindPathToOverload()
        {
            City city = DataGenerator.GenerateTestCity();

            // Проверка перегрузки метода поиска, который принимает город и объект учреждения, а не граф.
            // Результаты должны быть идентичны версии с графом.

            Graph graph = DataGenerator.GenerateTestCityGraph();
            PathInfo graphPathInfo = Finder.FindPathTo(graph, graph.Vertices.First(), 
                InstitutionType.Hospital);
            CityPathInfo cityPathInfo = Finder.FindPathTo(city, city.Institutions.First(), 
                InstitutionType.Hospital);

            Assert.AreEqual(graphPathInfo.Path.Length, cityPathInfo.Path.Length);
            Assert.AreEqual(graphPathInfo.TotalLength, cityPathInfo.TotalLength);
        }
    }
}