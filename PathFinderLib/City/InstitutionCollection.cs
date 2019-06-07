using System.Collections.ObjectModel;
using PathFinderLib.City.Institutions;

namespace PathFinderLib.City
{
    /// <summary>
    /// Коллекция (с доступом по ключу) учреждений.
    /// Ключ - свойство <see cref="Institution.Location"/>.
    /// </summary>
    public class InstitutionCollection : KeyedCollection<Point2D, Institution>
    {
        protected override Point2D GetKeyForItem(Institution item)
        {
            return item.Location;
        }
    }
}