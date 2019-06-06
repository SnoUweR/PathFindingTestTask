using System.Collections.ObjectModel;

namespace PathFinderLib.GraphEngine.Algorithms
{
    /// <summary>
    /// Коллекция (с доступом по ключу) с объектами информации для алгоритма Дейкстры по вершинам.
    /// Ключ - объект вершины.
    /// Значение - информация по вершине.
    /// </summary>
    public class DijkstraVertexInfoCollection : KeyedCollection<Vertex, DijkstraVertexInfo>
    {
        protected override Vertex GetKeyForItem(DijkstraVertexInfo item)
        {
            return item.Vertex;
        }
    }
}