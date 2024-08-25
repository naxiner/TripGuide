namespace TripGuide.Models
{
    public class TripRouteTouristObject
    {
        public Guid TripRouteId { get; set; }
        public TripRoute TripRoute { get; set; }

        public Guid TouristObjectId { get; set; }
        public TouristObject TouristObject { get; set; }
    }
}