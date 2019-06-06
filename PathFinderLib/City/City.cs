using System.Collections.Generic;
using System.Linq;
using PathFinderLib.City.Institutions;
using PathFinderLib.City.Institutions.Factories;

namespace PathFinderLib.City
{
    /// <summary>
    /// Город.
    /// </summary>
    public class City
    {
        /// <summary>
        /// Координаты верхнего левого угла прямоугольника (границ города).
        /// </summary>
        public Point2D TopLeftCorner { get; private set; }
        
        /// <summary>
        /// Координаты нижнего правого угла прямоугольника (границ города).
        /// </summary>
        public Point2D BottomRightCorner { get; private set; }
        
        /// <summary>
        /// Городские дороги (их координаты могут выходить за пределы границ города).
        /// </summary>
        public List<Road> Roads { get; private set; }
        
        /// <summary>
        /// Список учреждений, построенных в городе на пересечениях дорог друг с другом, и с границами
        /// самого города.
        /// Автоматически заполняется новыми пересечениями при каждом вызове метода <see cref="AddRoad"/>.
        /// </summary>
        public List<Institution> Institutions { get; private set; }

        /// <summary>
        /// Словарь, описывающий какие учреждения стоят на пересечении одной из конкретных дорог.
        /// Используется при нахождении «соседей» для каждого учреждения в <see cref="UpdateNeighbours"/>.
        /// Ключ - объект дороги.
        /// Значение - список учреждений, у которых в качестве FirstRoad или SecondRoad была установлена указанная
        /// в ключе дорога.
        /// </summary>
        private readonly Dictionary<Road, List<Institution>> _roadInstitutions;

        /// <summary>
        /// Экземпляр «фабрики» для создания учреждений в местах пересечений дорог.
        /// </summary>
        private readonly Builder _insitutionBuilder;

        /// <summary>
        /// Конструктор города.
        /// </summary>
        /// <param name="topLeftCorner">Левый верхний угол границ города.</param>
        /// <param name="bottomRightCorner">Нижний правый угол границ города.</param>
        /// <param name="insitutionBuilder">Экземпляр «фабрики» для создания учреждений в местах пересечений дорог.</param>
        public City(Point2D topLeftCorner, Point2D bottomRightCorner, Builder insitutionBuilder)
        {
            _insitutionBuilder = insitutionBuilder;

            Roads = new List<Road>();
            Institutions = new List<Institution>();
            _roadInstitutions = new Dictionary<Road, List<Institution>>();

            TopLeftCorner = topLeftCorner;
            BottomRightCorner = bottomRightCorner;

            /**
             * По факту, город — это 4 дороги, которые образуют собой прямоугольник, поэтому сразу добавляем
             * их в соответствующий список.
             */
            Point2D topRightCorner = new Point2D(bottomRightCorner.X, topLeftCorner.Y);
            Point2D bottomLeftCorner = new Point2D(topLeftCorner.X, bottomRightCorner.Y);
            AddRoad(new Road(topLeftCorner, topRightCorner));
            AddRoad(new Road(topRightCorner, bottomRightCorner));
            AddRoad(new Road(bottomRightCorner, bottomLeftCorner));
            AddRoad(new Road(bottomLeftCorner, topLeftCorner));
        }

        /// <summary>
        /// Добавляет новую дорогу в город, а также автоматически проверяет её пересечение
        /// с другими дорогами и границами города. Если пересечение найдется, то на это место будет
        /// добавлена новая постройка.
        /// Помимо этого, добавляет в список «соседей» уже существующих построек новую.
        /// </summary>
        /// <param name="newRoad">Новая дорогая для добавления.</param>
        public void AddRoad(Road newRoad)
        {
            foreach (Road existedRoad in Roads)
            {
                if (existedRoad.TryGetIntersectPoint(newRoad, out var intersectPoint))
                {
                    var newInstitution = _insitutionBuilder.Build(existedRoad, newRoad,
                        intersectPoint);
                    
                    Institutions.Add(newInstitution);

                    UpdateNeighbours(existedRoad, newInstitution, intersectPoint);
                    UpdateNeighbours(newRoad, newInstitution, intersectPoint);
                }
            }
            
            Roads.Add(newRoad);
        }

        /// <summary>
        /// Производит обновление соседей для всех учреждений, которые связаны
        /// с дорогой <paramref name="roadWithNeighbours"/>.
        /// Если у учерждений есть общая дорога, то значит они «соседствуют». Данная функция проверяет
        /// уже существующих соседей у всех учреждений дороги.
        /// Если у какого-либо учреждения еще нет соседей, то новая постройка сразу добавляется в качестве соседа.
        /// Если же соседи уже существуют, то сначала проверяется, находится ли сосед на той же дороге, что и
        /// новая постройка. Если нет, то постройка также добавляется в качестве соседа.
        /// Если существующие соседи находятся на той же дороге, что и новая постройка, то
        /// в таком случае проверяется местоположение новой постройки относительно постройки, у которой проверяем
        /// соседей, а также каждого из соседей. Если новая постройка находится «между» этими двумя объектами, то
        /// считаем, что уже существующий сосед перестал быть таковым, удаляем его из списка, и добавляем вместо него
        /// нашу новую постройку.
        /// </summary>
        /// <param name="roadWithNeighbours">Объект дороги, которая связана с <paramref name="newInstitution"/>, и
        /// у существующих учреждений на которой необходимо обновить соседей.</param>
        /// <param name="newInstitution">Новая постройка, одной из дорог
        /// которой является <paramref name="roadWithNeighbours"/></param>
        /// <param name="intersectPoint">Точка пересечения дороги <paramref name="roadWithNeighbours"/> с некой
        /// другой дорогой, вследствие чего был создан <paramref name="newInstitution"/></param>
        private void UpdateNeighbours(Road roadWithNeighbours, Institution newInstitution, Point2D intersectPoint)
        {
            // Если на данной дороге еще не было никаких учреждений, то необходимо инициализировать для неё их список.
            if (!_roadInstitutions.ContainsKey(roadWithNeighbours))
            {
                _roadInstitutions[roadWithNeighbours] = new List<Institution>();
            }

            foreach (Institution existedInstitution in _roadInstitutions[roadWithNeighbours])
            {
                // Если у какой-либо из построек на дороге еще нет соседей, то можно сразу добавлять туда нашу новую.
                if (existedInstitution.Neighbours.Count == 0)
                {
                    existedInstitution.AddNeighbour(newInstitution);
                    newInstitution.AddNeighbour(existedInstitution);
                    continue;
                }

                bool neighbourOnSameLineExists = false;

                // Так как функции ReplaceNeighbour и AddNeighbour меняют содержимое Neighbours, то
                // «ходим» по копии этого объекта.
                foreach (Institution neighbour in existedInstitution.Neighbours.ToList())
                {
                    // Игнорируем тех «соседей», которые находятся на другой дороге, и которые никак не мешают нам.
                    if (neighbour.FirstRoad != roadWithNeighbours && neighbour.SecondRoad != roadWithNeighbours)
                    {
                        continue;
                    }

                    neighbourOnSameLineExists = true;

                    // Если наша новая постройка находится между некой постройкой и её соседом (на одной дороге), то
                    // значит мы «разорвали» соседство, и теперь сами являемся соседом той постройки.
                    if (intersectPoint.IsBetweenTwoPointsOnSameLine(existedInstitution.Location,
                        neighbour.Location))
                    {
                        existedInstitution.ReplaceNeighbour(neighbour, newInstitution);
                        newInstitution.AddNeighbour(existedInstitution);
                        break;
                    }
                }

                // Если нет никаких построек между нашей новой постройкой и той, которую обрабатываем в цикле foreach, 
                // то считаем нас её соседом.
                if (!neighbourOnSameLineExists)
                {
                    existedInstitution.AddNeighbour(newInstitution);
                    newInstitution.AddNeighbour(existedInstitution);
                }
            }
            
            _roadInstitutions[roadWithNeighbours].Add(newInstitution);
        }
    }
}