using System;
using System.Collections.Generic;

namespace PathFinderLib.City.Institutions.Factories
{
    /// <summary>
    /// Фабрика объектов учреждений, которая «строит» их в случайном порядке.
    /// </summary>
    public class RandomBuilder : Builder
    {
        /// <summary>
        /// Экземпляр ГПСЧ.
        /// </summary>
        private readonly Random _randomInstance;

        /// <summary>
        /// Конструктор фабрики, которая строит учреждения в случайном порядке.
        /// </summary>
        public RandomBuilder()
        {
            _randomInstance = new Random();
        }

        /// <summary>
        /// Конструктор фабрики, которая строит учреждения в случайном порядке.
        /// Случайный порядок задается указанным seed'ом, которым будет инициализирован ГПСЧ.
        /// </summary>
        /// <param name="seed">seed для инициализации ГПСЧ.</param>
        public RandomBuilder(int seed)
        {
            _randomInstance = new Random(seed);
        }
        
        /// <summary>
        /// Метод для «постройки» объекта с учреждением. Тип учреждения выбирается случайным образом.
        /// </summary>
        /// <param name="firstRoad">Первая из двух дорог, на пересечении которых будет располагаться учреждение.</param>
        /// <param name="secondRoad">Вторая из двух дорог, на пересечении которых будет располагаться учреждение.</param>
        /// <param name="location">Координаты пересечения.</param>
        /// <returns>Экземпляр конкретного объекта с учреждением.</returns>
        public override Institution Build(Road firstRoad, Road secondRoad, Point2D location)
        {
            const int institutionTypesCount = 3;
            int randomNumber = _randomInstance.Next(1, institutionTypesCount + 1);
            switch (randomNumber)
            {
                case 1:
                    return new Hospital(firstRoad, secondRoad, location);
                case 2:
                    return new PoliceDepartment(firstRoad, secondRoad, location);
                case 3:
                    return new PostOffice(firstRoad, secondRoad, location);
                default:
                    throw new Exception($"Unknown Institution type with number {randomNumber}");
            }
        }
    }
}