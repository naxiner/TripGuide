namespace TripGuide.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<BlogPost> Blogs { get; set; }
        public List<Tag> Tags { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}