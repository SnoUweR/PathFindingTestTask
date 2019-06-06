namespace PathFinderLib.GraphEngine
{
    /// <summary>
    /// Ребро графа.
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// Связанная с ребром вершина.
        /// </summary>
        public Vertex ConnectedVertex { get; private set; }

        /// <summary>
        /// Вес ребра.
        /// </summary>
        public float EdgeWeight { get; private set; }

        /// <summary>
        /// Конструктор ребра.
        /// </summary>
        /// <param name="connectedVertex">Связанная с ребром вершина.</param>
        /// <param name="weight">Вес ребра.</param>
        public Edge(Vertex connectedVertex, float weight)
        {
            ConnectedVertex = connectedVertex;
            EdgeWeight = weight;
        }
    }
}