namespace TripGuide.Models
{
    public class TouristObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }

        public List<TripRouteTouristObject> TripRouteTouristObjects { get; set; }
    }
}