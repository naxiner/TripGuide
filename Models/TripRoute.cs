namespace TripGuide.Models
{
    public class TripRoute
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<TripRouteTouristObject> TripRouteTouristObjects { get; set; } = new List<TripRouteTouristObject>();
    }
}