namespace TripGuide.Models
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public Guid UserId { get; set; }
        public Guid ObjectId { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}