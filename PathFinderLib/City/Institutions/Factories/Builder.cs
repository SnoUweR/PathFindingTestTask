namespace PathFinderLib.City.Institutions.Factories
{
    /// <summary>
    /// Базовый класс для фабрики объектов учреждений.
    /// </summary>
    public abstract class Builder
    {
        /// <summary>
        /// Метод для «постройки» конкретного объекта с учреждением.
        /// </summary>
        /// <param name="firstRoad">Первая из двух дорог, на пересечении которых будет располагаться учреждение.</param>
        /// <param name="secondRoad">Вторая из двух дорог, на пересечении которых будет располагаться учреждение.</param>
        /// <param name="location">Координаты пересечения.</param>
        /// <returns>Экземпляр конкретного объекта с учреждением.</returns>
        public abstract Institution Build(Road firstRoad, Road secondRoad, Point2D location);
    }
}