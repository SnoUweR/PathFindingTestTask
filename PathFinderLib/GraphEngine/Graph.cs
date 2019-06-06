using System.Collections.Generic;

namespace PathFinderLib.GraphEngine
{
    /// <summary>
    /// Граф.
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// Список вершин графа.
        /// </summary>
        public List<Vertex> Vertices { get; private set; }

        /// <summary>
        /// Конструктор графа.
        /// </summary>
        public Graph()
        {
            Vertices = new List<Vertex>();
        }

        /// <summary>
        /// Добавляет новую вершину в граф, и задает ей в соответствие указанный объект.
        /// </summary>
        /// <param name="tag">Некий объект, соответствующий данной вершине.</param>
        public Vertex AddVertex(object tag)
        {
            Vertex vertex = new Vertex(tag);
            Vertices.Add(vertex);
            return vertex;
        }

        /// <summary>
        /// Добавляет новое ребро в граф, а также задает ему определенный вес.
        /// Ребро добавляется в обе указанных вершины, поэтому данную функцию следует
        /// вызывать только один раз для пары вершин, а не два раза для каждой из них.
        /// </summary>
        /// <param name="firstVertex">Первая вершина, связанная ребром.</param>
        /// <param name="secondVertex">Вторая вершина, связанная ребром.</param>
        /// <param name="weight">Вес ребра, соединяющего вершины.</param>
        public void AddEdge(Vertex firstVertex, Vertex secondVertex, float weight)
        {
            firstVertex.AddEdge(secondVertex, weight);
            secondVertex.AddEdge(firstVertex, weight);
        }
    }
}