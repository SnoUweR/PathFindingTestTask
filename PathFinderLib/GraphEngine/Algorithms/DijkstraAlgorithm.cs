using System.Collections.Generic;

namespace PathFinderLib.GraphEngine.Algorithms
{
    /// <summary>
    /// Реализация алгоритма Дейкстры для поиска кратчайшего пути.
    /// </summary>
    public class DijkstraAlgorithm
    {
        /// <summary>
        /// Граф, по которому будет осуществляться поиск.
        /// </summary>
        private readonly Graph _graph;

        /// <summary>
        /// Коллекция (с доступом по ключу) с объектами информации по всем вершинам графа.
        /// Ключ - экземпляр Vertex, информация по которому нужна.
        /// </summary>
        private readonly DijkstraVertexInfoCollection _verticesInfo;

        /// <summary>
        /// Конструктор алгоритма.
        /// </summary>
        /// <param name="graph">Заполненный граф с вершинами ребрами.</param>
        public DijkstraAlgorithm(Graph graph)
        {
            _graph = graph;
            _verticesInfo = new DijkstraVertexInfoCollection();
        }

        /// <summary>
        /// Производит инициализацию информации по всем вершинам графа.
        /// Если поиск выполняется повторно, то есть в списке информации по вершинам уже
        /// есть элементы, то просто сбрасывает их значения на стандартные, не создавая новые объекты.
        /// </summary>
        private void InitVerticesData()
        {
            if (_verticesInfo.Count > 0)
            {
                foreach (DijkstraVertexInfo vertexInfo in _verticesInfo)
                {
                    vertexInfo.ResetData();
                }
            }
            else
            {
                foreach (Vertex vertex in _graph.Vertices)
                {
                    _verticesInfo.Add(new DijkstraVertexInfo(vertex));
                }
            }
        }

        /// <summary>
        /// Пытается получить информацию по указанной вершине из коллекции <see cref="_verticesInfo"/>.
        /// Если по какой-либо причине информация не найдена, то возвращает false,
        /// а <paramref name="dijkstraVertexInfo"/> устанавливается в стандартное значение.
        /// </summary>
        /// <param name="vertex">Вершина, соответствующий объект DijkstraVertexInfo которой необходимо получить.</param>
        /// <param name="dijkstraVertexInfo">Возвращаемая информация по вершине.</param>
        /// <returns>true, если информация найдена.</returns>
        private bool TryGetVertexInfo(Vertex vertex, out DijkstraVertexInfo dijkstraVertexInfo)
        {
            dijkstraVertexInfo = default(DijkstraVertexInfo);

            if (_verticesInfo.Contains(vertex))
            {
                dijkstraVertexInfo = _verticesInfo[vertex];
                return true;
            }
            return false;
        }

        /// <summary>
        /// Осуществляет поиск непосещенной вершины с минимальной суммой весов ребер, а также
        /// возвращает её, если такая вершина найдена.
        /// </summary>
        /// <returns>Объект с информацией о вершине. null, если вершина не найдена.</returns>
        private DijkstraVertexInfo FindUnvisitedVertexWithMinSum()
        {
            var minValue = float.MaxValue;
            DijkstraVertexInfo minVertexInfo = null;
            foreach (DijkstraVertexInfo vertexInfo in _verticesInfo)
            {
                if (!vertexInfo.IsVisited && vertexInfo.TotalEdgesWeight < minValue)
                {
                    minVertexInfo = vertexInfo;
                    minValue = vertexInfo.TotalEdgesWeight;
                }
            }

            return minVertexInfo;
        }
        
        /// <summary>
        /// Осуществляет поиск кратчайшего пути в графе.
        /// Поиск начинается с вершины <paramref name="startVertex"/>, путь ищется до вершины
        /// <paramref name="finishVertex"/>.
        /// Если начальная и конечная вершины равны между собой, то возвращает пустой путь.
        /// Если путь не найден, то также возвращает пустой путь.
        /// </summary>
        /// <param name="startVertex">Стартовая вершина.</param>
        /// <param name="finishVertex">Финишная вершина, путь до которой нужно найти.</param>
        /// <returns>Объект с информацией о найденном пути.</returns>
        public PathInfo FindShortestPath(Vertex startVertex, Vertex finishVertex)
        {
            InitVerticesData();
            if (!TryGetVertexInfo(startVertex, out var first))
            {
                return PathInfo.CreateEmptyPath();
            }
            
            first.TotalEdgesWeight = 0;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }

                SetSumToNextVertex(current);
            }

            TryGetVertexInfo(finishVertex, out var last);
            // Если искомую вершину мы так и не посетили, значит пути до неё нет.
            if (!last.IsVisited)
            {
                return PathInfo.CreateEmptyPath();
            }
            return GeneratePath(startVertex, finishVertex);
        }

        /// <summary>
        /// Вычисляет сумму весов ребер для всех вершин, которые связаны с указанной.
        /// </summary>
        /// <param name="info">Информация о текущей вершине.</param>
        private void SetSumToNextVertex(DijkstraVertexInfo info)
        {
            info.IsVisited = true;
            foreach (var e in info.Vertex.Edges)
            {
                TryGetVertexInfo(e.ConnectedVertex, out var nextInfo);
                var sum = info.TotalEdgesWeight + e.EdgeWeight;
                if (sum < nextInfo.TotalEdgesWeight)
                {
                    nextInfo.TotalEdgesWeight = sum;
                    nextInfo.PathEdgesWeight = e.EdgeWeight;
                    nextInfo.PreviousVertex = info.Vertex;
                }
            }
        }

        /// <summary>
        /// Формирует путь от <paramref name="startVertex"/> до <paramref name="endVertex"/>, исходя
        /// из уже заполненной в <see cref="_verticesInfo"/> информации о вершинах.
        /// Метод должен вызываться после всех проходов по графу.
        /// </summary>
        /// <param name="startVertex">Стартовая вершина.</param>
        /// <param name="endVertex">Финишная вершина, путь до которой нужно найти.</param>
        /// <returns>Объект с информацией о найденном пути.</returns>
        private PathInfo GeneratePath(Vertex startVertex, Vertex endVertex)
        {
            float totalPathLength = 0;
            List<Vertex> verticesInPath = new List<Vertex>();
            while (true)
            {
                TryGetVertexInfo(endVertex, out var vertexInfo);
                verticesInPath.Add(endVertex);
                totalPathLength += vertexInfo.PathEdgesWeight;
                if (startVertex == endVertex) break;
                endVertex = vertexInfo.PreviousVertex;
            }

            verticesInPath.Reverse();
            PathInfo pathInfo = new PathInfo(verticesInPath.ToArray(), totalPathLength);
            return pathInfo;
        }
    }
}