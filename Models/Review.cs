namespace TripGuide.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid UserId { get; set; }
        public Guid BlogPostId { get; set; }
    }
}