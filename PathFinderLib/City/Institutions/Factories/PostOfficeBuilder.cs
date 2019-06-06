namespace PathFinderLib.City.Institutions.Factories
{
    /// <summary>
    /// Фабрика объектов учреждений, которая «строит» исключительно объекты типа «Почтовое отделение».
    /// </summary>
    public class PostOfficeBuilder : Builder
    {
        /// <summary>
        /// Метод для «постройки» учреждения типа «Почтовое отделение».
        /// </summary>
        /// <param name="firstRoad">Первая из двух дорог, на пересечении которых будет располагаться учреждение.</param>
        /// <param name="secondRoad">Вторая из двух дорог, на пересечении которых будет располагаться учреждение.</param>
        /// <param name="location">Координаты пересечения.</param>
        /// <returns>Экземпляр конкретного объекта с учреждением.</returns>
        public override Institution Build(Road firstRoad, Road secondRoad, Point2D location)
        {
            return new PostOffice(firstRoad, secondRoad, location);
        }
    }
}