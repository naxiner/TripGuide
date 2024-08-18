using System.ComponentModel.DataAnnotations.Schema;

namespace TripGuide.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }
        public Guid BlogPostId { get; set; }

        [NotMapped]
        public string? UserName { get; set; }
    }
}