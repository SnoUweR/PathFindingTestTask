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
        
        public HashSet<Institution> Neighbours { get; private set; }
        
        public Institution(Road firstRoad, Road secondRoad, Point2D location)
        {
            FirstRoad = firstRoad;
            SecondRoad = secondRoad;
            Location = location;

            Neighbours = new HashSet<Institution>();
        }

        public void AddNeighbour(Institution institution)
        {
            //Console.WriteLine($"К {Location} добавляем соседа {institution.Location}");
            Neighbours.Add(institution);
        }

        public void ReplaceNeighbour(Institution oldNeighbour, Institution newNeighbour)
        {
           // Console.WriteLine($"К {Location} заменяем соседа  на {newNeighbour.Location}");
            Neighbours.Remove(oldNeighbour);
            Neighbours.Add(newNeighbour);
        }

        public override string ToString()
        {
            return $"{GetType().Name}:{Location}";
        }
    }
}