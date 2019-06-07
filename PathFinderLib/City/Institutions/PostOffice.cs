namespace PathFinderLib.City.Institutions
{
    /// <summary>
    /// Почтовое отделение.
    /// </summary>
    public class PostOffice : Institution
    {
        public PostOffice(Road firstRoad, Road secondRoad, Point2D location) : base(firstRoad, secondRoad, location)
        {
            
        }

        public override InstitutionType GetInstitutionType()
        {
            return InstitutionType.PostOffice;
        }
    }
}