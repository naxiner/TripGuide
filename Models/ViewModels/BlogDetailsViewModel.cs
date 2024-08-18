namespace TripGuide.Models.ViewModels
{
    public class BlogDetailsViewModel
    {
        public BlogPost BlogPost { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}