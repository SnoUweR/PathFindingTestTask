using System.Collections.Generic;

namespace PathFinderLib.GraphEngine
{
    /// <summary>
    /// Вершина.
    /// </summary>
    public class Vertex
    {
        /// <summary>
        /// Некий объект, соответствующий данной вершине.
        /// </summary>
        public object Tag { get; private set; }
        
        /// <summary>
        /// Список ребер вершины.
        /// </summary>
        public List<Edge> Edges { get; private set; }

        /// <summary>
        /// Конструктор вершины.
        /// </summary>
        /// <param name="tag">Некий объект, соответствующий данной вершине.</param>
        public Vertex(object tag)
        {
            Edges = new List<Edge>();
            Tag = tag;
        }

        /// <summary>
        /// Добавляет ребро в вершину, а также задает, с какой другой вершиной это ребро
        /// соединяет.
        /// </summary>
        /// <param name="vertex">Другая вершина, связанная этим ребром.</param>
        /// <param name="edgeWeight">Вес ребра.</param>
        public void AddEdge(Vertex vertex, float edgeWeight)
        {
            Edges.Add(new Edge(vertex, edgeWeight));
        }
        
        /// <summary>
        /// Выводит строкое представление объекта <see cref="Tag"/>, который задан в соответствие данной вершине.
        /// </summary>
        /// <returns>Строкое представление объекта <see cref="Tag"/>,
        /// который задан в соответствие данной вершине.</returns>
        public override string ToString() => Tag.ToString();
    }
}