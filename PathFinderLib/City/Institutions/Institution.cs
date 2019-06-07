using System;
using System.Collections.Generic;

namespace PathFinderLib.City.Institutions
{
    /// <summary>
    /// Учреждение.
    /// </summary>
    public abstract class Institution
    {
        /// <summary>
        /// Координаты данного учреждения.
        /// </summary>
        public Point2D Location { get; private set; }
        
        /// <summary>
        /// Первая дорога из двух, на пересечении которых стоит данное учреждение.
        /// </summary>
        public Road FirstRoad { get; private set; }
 
        /// <summary>
        /// Вторая дорога из двух, на пересечении которых стоит данное учреждение.
        /// </summary>
        public Road SecondRoad { get; private set; }
        
        /// <summary>
        /// Хэш-список с соседями данного учреждения.
        /// Если текущее учреждение имеет общую дорогу с каким-либо другим учреждением,
        /// и на их пути нет других объектов, то они считаются соседями.
        /// </summary>
        public HashSet<Institution> Neighbours { get; private set; }
        
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="firstRoad">Первая дорога из двух, на пересечении которых стоит данное учреждение.</param>
        /// <param name="secondRoad">Вторая дорога из двух, на пересечении которых стоит данное учреждение.</param>
        /// <param name="location">Координаты данного учреждения.</param>
        protected Institution(Road firstRoad, Road secondRoad, Point2D location)
        {
            FirstRoad = firstRoad;
            SecondRoad = secondRoad;
            Location = location;

            Neighbours = new HashSet<Institution>();
        }

        /// <summary>
        /// Добавляет указанное учреждение в качестве соседа.
        /// Добавляет однонаправленно, то есть только в текущий объект.
        /// </summary>
        /// <param name="institution">Объект учреждения, которое нужно добавить в качестве соседа.</param>
        public void AddNeighbour(Institution institution)
        {
            Neighbours.Add(institution);
        }

        /// <summary>
        /// Удаляет из списка текущих соседей <paramref name="oldNeighbour"/>, и добавляет
        /// в список <paramref name="newNeighbour"/>
        /// </summary>
        /// <param name="oldNeighbour">Объект учреждения, которое нужно удалить из списка соседей.</param>
        /// <param name="newNeighbour">Объект учреждения, которое нужно добавить в список соседей.</param>
        public void ReplaceNeighbour(Institution oldNeighbour, Institution newNeighbour)
        {
            Neighbours.Remove(oldNeighbour);
            Neighbours.Add(newNeighbour);
        }

        /// <summary>
        /// Возвращает тип данного учреждения.
        /// </summary>
        /// <returns>Тип данного учреждения.</returns>
        public abstract InstitutionType GetInstitutionType();

        /// <summary>
        /// Выводит информацию о текущем учреждении в виде «ТИП УЧРЕЖДЕНИЯ;МЕСТОПОЛОЖЕНИЕ».
        /// </summary>
        /// <returns>Информацию о текущем учреждении в виде «ТИП УЧРЕЖДЕНИЯ;МЕСТОПОЛОЖЕНИЕ».</returns>
        public override string ToString()
        {
            return $"{GetType().Name}:{Location}";
        }
    }
}