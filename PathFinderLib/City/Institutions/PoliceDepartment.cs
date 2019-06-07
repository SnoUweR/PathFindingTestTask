namespace PathFinderLib.City.Institutions
{
    /// <summary>
    /// Полицейский участок.
    /// </summary>
    public class PoliceDepartment : Institution
    {
        public PoliceDepartment(Road firstRoad, Road secondRoad, Point2D location) : base(firstRoad, secondRoad,
            location)
        {

        }

        public override InstitutionType GetInstitutionType()
        {
            return InstitutionType.PoliceDepartment;
        }
    }
}