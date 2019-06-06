using System.Text;

namespace PathFinderLib.GraphEngine.Algorithms
{
    /// <summary>
    /// Информация по найденному кратчайшему пути.
    /// </summary>
    public class PathInfo
    {
        /// <summary>
        /// Массив с вершинами, которые входят в кратчайший путь.
        /// Расположены в порядке следования пути.
        /// </summary>
        public Vertex[] Path { get; private set; }
        
        /// <summary>
        /// Общая «продолжительность» пути. По факту, сумма весов ребер, которыми связываются
        /// элементы из <see cref="Path"/>.
        /// </summary>
        public float TotalLength { get; private set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="path">Массив с вершинами, которые входят в кратчайший путь.
        /// Элементы должны располагаться уже в правильном порядке (от начальной до конечной точки).</param>
        /// <param name="totalLength">Общая «продолжительность» пути.</param>
        public PathInfo(Vertex[] path, float totalLength)
        {
            Path = path;
            TotalLength = totalLength;
        }

        /// <summary>
        /// Создает объект с пустым путем, соответствующий значению «ПУТЬ НЕ НАЙДЕН».
        /// </summary>
        /// <returns>Объект с пустым путем.</returns>
        public static PathInfo CreateEmptyPath()
        {
            return new PathInfo(new Vertex[0], 0);
        }

        /// <summary>
        /// Определяет, является ли данный путь пустым.
        /// </summary>
        /// <returns>true, если данный путь пустой.</returns>
        public bool IsEmptyPath()
        {
            return Path.Length == 0;
        }

        /// <summary>
        /// Переопределение функции вывода в виде строки.
        /// Возвращает [Empty Path], если данный путь пустой, и
        /// маршрут (в виде {вершина} -> ... -> {вершина}), если путь не пустой.
        /// </summary>
        /// <returns>Строковое представление данного объекта.</returns>
        public override string ToString()
        {
            if (IsEmptyPath())
            {
                return "[Empty Path]";
            }

            StringBuilder sb = new StringBuilder();
            const string separator = "->";
            for (var i = 0; i < Path.Length; i++)
            {
                Vertex vertex = Path[i];
                sb.Append(vertex);
                if (i < Path.Length - 1)
                {
                    sb.Append(separator); 
                }

            }

            return sb.ToString();
        }
    }
}