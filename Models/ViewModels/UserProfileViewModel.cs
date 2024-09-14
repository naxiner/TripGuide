namespace TripGuide.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public User User { get; set; }
        public List<UserBlogPost> Collections { get; set; }
        public List<Review> Reviews { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
