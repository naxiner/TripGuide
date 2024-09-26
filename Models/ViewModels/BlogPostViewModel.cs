namespace TripGuide.Models.ViewModels
{
    public class BlogPostViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
