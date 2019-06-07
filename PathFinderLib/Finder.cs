using System;
using System.Collections.Generic;
using System.Linq;
using PathFinderLib.City;
using PathFinderLib.City.Institutions;
using PathFinderLib.City.Institutions.Factories;
using PathFinderLib.GraphEngine;
using PathFinderLib.GraphEngine.Algorithms;

namespace PathFinderLib
{
    /// <summary>
    /// Класс, позволяющий производить определенные действия над городом <see cref="City"/>.
    /// </summary>
    public abstract class Finder
    {
        /// <summary>
        /// Преобразовывает объект города в граф, где
        /// вершины - точки пересечений дорог;
        /// ребра - дороги.
        /// </summary>
        /// <param name="city">Объект с городом.</param>
        /// <returns>Граф, соответствующий указанному объекту с городом.</returns>
        public static Graph ConvertCityToGraph(City.City city)
        {
            Dictionary<Institution, Vertex> processingInstitutions = new Dictionary<Institution, Vertex>();
            Dictionary<Institution, Vertex> processedInstitutions = new Dictionary<Institution, Vertex>();
            Graph graph = new Graph();
            foreach (var institution in city.Institutions)
            {
                AddVertex(graph, null, institution, processingInstitutions, processedInstitutions);
            }
            
            return graph;
        }
        
        private static Vertex AddVertex(Graph graph, Institution parent, Institution institution,
            Dictionary<Institution, Vertex> processingItems, Dictionary<Institution, Vertex> processedItems)
        {
            if (processedItems.ContainsKey(institution)) return null;
            if (processingItems.ContainsKey(institution)) return processingItems[institution];
            Vertex vertex = graph.AddVertex(institution);
            processingItems[institution] = vertex;
            foreach (Institution neighbour in institution.Neighbours)
            {
                if (neighbour != parent)
                {
                    Vertex neighbourVertex = AddVertex(
                        graph, institution, neighbour, processingItems, processedItems);
                    if (neighbourVertex == null) continue;
                    float distance = Point2D.GetPointsDistance(institution.Location, neighbour.Location);
                    graph.AddEdge(vertex, neighbourVertex, distance);
                }
            }
            processedItems[institution] = vertex;
            return vertex;
        }
        
           /// <summary>
        /// Осуществляет поиск кратчайшего пути до необходимого типа постройки в «городе».
        /// Если построек такого типа несколько, то возвращает путь только до той, который оказался самым кратчайшим.
        /// Если не удалось найти, то возвращает пустой путь.
        /// </summary>
        /// <param name="graph">Граф, соответствующий городу, в котором будем искать путь.</param>
        /// <param name="startPoint">Стартовая точка, из которой ищем путь.</param>
        /// <param name="institutionType">Тип постройки, к которой ищем путь.</param>
        /// <returns>Объект с информацией о найденном или не найденном пути.</returns>
        public static PathInfo FindPathTo(Graph graph, Vertex startPoint, InstitutionType institutionType)
        {
            PathInfo shortestPath = PathInfo.CreateEmptyPath();
            float shortestPathLength = float.MaxValue;
            
            /**
             * Для нахождения вершин в графе, которые соответствуют нужной нам постройке,
             * осуществляется проход по всем вершинам, и ищется объект нужного нам типа.
             * Далее вызывается алгоритм Дейкстры до найденной вершины. Так повторяется до тех пор, пока
             * не пройдемся по всем нужным нам вершинам. Далее среди всех найденных путей ищется тот, который
             * является наикратчайшим.
             */
            
            foreach (Vertex vertex in graph.Vertices)
            {
                // Стартовая точка не может оказаться конечной.
                if (vertex == startPoint) continue;
                
                Institution institution = vertex.Tag as Institution;
                if (institution != null && institution.GetInstitutionType() == institutionType)
                {
                    DijkstraAlgorithm dijkstraAlgorithm = new DijkstraAlgorithm(graph);
                    PathInfo pathInfo = dijkstraAlgorithm.FindShortestPath(startPoint, vertex);
                    if (pathInfo.TotalLength < shortestPathLength)
                    {
                        shortestPath = pathInfo;
                        shortestPathLength = pathInfo.TotalLength;
                    }
                }
            }

            return shortestPath;
        }

        /// <summary>
        /// Осуществляет поиск кратчайшего пути до необходимого типа постройки в «городе».
        /// Если построек такого типа несколько, то возвращает путь только до той, который оказался самым кратчайшим.
        /// Если не удалось найти, то возвращает пустой путь.
        /// </summary>
        /// <param name="city">Объект с городом, поиск пути в котором необходимого выполнить.</param>
        /// <param name="startInstitution">Стартовый объект, из которой ищем путь.</param>
        /// <param name="institutionType">Тип постройки, к которой ищем путь.</param>
        /// <returns>Объект с информацией о найденном или не найденном пути.</returns>
        public static CityPathInfo FindPathTo(City.City city, Institution startInstitution,
            InstitutionType institutionType)
        {
            Graph graph = ConvertCityToGraph(city);
            Vertex startVertex = graph.Vertices.FirstOrDefault(vertex => vertex.Tag == startInstitution);
            if (startVertex == null)
            {
                throw new ArgumentException("'startInstitution' is not found in the 'city' object",
                    nameof(startInstitution));
            }

            
            PathInfo pathInfo = FindPathTo(graph, startVertex, institutionType);
            CityPathInfo cityPathInfo = new CityPathInfo(
                pathInfo.Path.Select(vertex => vertex.Tag as Institution).ToArray(),
                pathInfo.TotalLength);
            return cityPathInfo;
        }
    }
}