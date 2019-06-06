using System;
using System.Linq;
using NUnit.Framework;
using PathFinderLib;
using PathFinderLib.City;
using PathFinderLib.GraphEngine;
using PathFinderLib.GraphEngine.Algorithms;

namespace PathFinderTests
{
    [TestFixture]
    public class DijkstraTests
    {
        [Test]
        public void TestFindShortestPath1()
        {
            Graph graph = DataGenerator.GenerateTestCityGraph();
            DijkstraAlgorithm dijkstraAlgorithm = new DijkstraAlgorithm(graph);
            
            Vertex startVertex = graph.Vertices[0]; // 10;0
            Vertex endVertex = graph.Vertices[8]; // 0;0
            PathInfo pathInfo = dijkstraAlgorithm.FindShortestPath(startVertex, endVertex);
            Assert.AreEqual(5, pathInfo.Path.Length);
            Assert.AreEqual(10, pathInfo.TotalLength);
            
            startVertex = graph.Vertices[1]; // 10;10
            endVertex = graph.Vertices[2]; // 10;11
            pathInfo = dijkstraAlgorithm.FindShortestPath(startVertex, endVertex);
            Assert.AreEqual(2, pathInfo.Path.Length);
            Assert.AreEqual(1, pathInfo.TotalLength);
        }
    }
}