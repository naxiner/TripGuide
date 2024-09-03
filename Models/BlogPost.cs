using System.ComponentModel.DataAnnotations.Schema;

namespace TripGuide.Models
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }

        [NotMapped]
        public IFormFile? FeaturedImage { get; set; }

        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public Guid? TouristObjectId { get; set; }
        public TouristObject? TouristObject { get; set; }
        public Guid? TripRouteId { get; set; }
        public TripRoute? TripRoute { get; set; }
        public ICollection<UserBlogPost>? UserBlogPosts { get; set; }
    }
}