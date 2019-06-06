namespace PathFinderLib.City.Institutions.Factories
{
    /// <summary>
    /// Фабрика объектов учреждений, которая «строит» исключительно объекты типа «Полицейский участок».
    /// </summary>
    public class PoliceDepartmentBuilder : Builder
    {
        /// <summary>
        /// Метод для «постройки» учреждения типа «Полицейский участок».
        /// </summary>
        /// <param name="firstRoad">Первая из двух дорог, на пересечении которых будет располагаться учреждение.</param>
        /// <param name="secondRoad">Вторая из двух дорог, на пересечении которых будет располагаться учреждение.</param>
        /// <param name="location">Координаты пересечения.</param>
        /// <returns>Экземпляр конкретного объекта с учреждением.</returns>
        public override Institution Build(Road firstRoad, Road secondRoad, Point2D location)
        {
            return new PoliceDepartment(firstRoad, secondRoad, location);
        }
    }
}