namespace PathFinderLib.City.Institutions
{
    /// <summary>
    /// Больница.
    /// </summary>
    public class Hospital : Institution
    {
        public Hospital(Road firstRoad, Road secondRoad, Point2D location) : base(firstRoad, secondRoad, location)
        {
        }
    }
}