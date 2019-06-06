namespace PathFinderLib.GraphEngine.Algorithms
{
    /// <summary>
    /// Описание вершины для алгоритма Дейкстры.
    /// </summary>
    public class DijkstraVertexInfo
    {
        /// <summary>
        /// Устанавливается в true, когда алгоритм «посетил» данную вершину.
        /// </summary>
        public bool IsVisited { get; set; }
        
        /// <summary>
        /// Общая сумма весов ребер данной вершины.
        /// </summary>
        public float TotalEdgesWeight { get; set; }
        
        /// <summary>
        /// Вес ребра, которое было выбрано в качестве кратчайшего маршрута.
        /// </summary>
        public float PathEdgesWeight { get; set; }
        
        /// <summary>
        /// Объект вершины, соответствующий данной информации.
        /// </summary>
        public Vertex Vertex { get; private set; }
        
        /// <summary>
        /// Предыдущая посещенная вершина.
        /// </summary>
        public Vertex PreviousVertex { get; set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="vertex">Объект вершины, которому будет соответствовать информация.</param>
        public DijkstraVertexInfo(Vertex vertex)
        {
            Vertex = vertex;
            ResetData();
        }

        /// <summary>
        /// Осуществляет сброс информации в стандартные значения.
        /// Метод должен использоваться перед каждым повторным нахождением пути.
        /// </summary>
        public void ResetData()
        {
            TotalEdgesWeight = float.MaxValue;
            PreviousVertex = null;
            PathEdgesWeight = 0;
            IsVisited = false;
        }
    }
}