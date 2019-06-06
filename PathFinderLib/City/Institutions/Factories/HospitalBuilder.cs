namespace PathFinderLib.City.Institutions.Factories
{
    /// <summary>
    /// Фабрика объектов учреждений, которая «строит» исключительно объекты типа «Больница».
    /// </summary>
    public class HospitalBuilder : Builder
    {
        /// <summary>
        /// Метод для «постройки» учреждения типа «Больница».
        /// </summary>
        /// <param name="firstRoad">Первая из двух дорог, на пересечении которых будет располагаться учреждение.</param>
        /// <param name="secondRoad">Вторая из двух дорог, на пересечении которых будет располагаться учреждение.</param>
        /// <param name="location">Координаты пересечения.</param>
        /// <returns>Экземпляр конкретного объекта с учреждением.</returns>
        public override Institution Build(Road firstRoad, Road secondRoad, Point2D location)
        {
            return new Hospital(firstRoad, secondRoad, location);
        }
    }
}