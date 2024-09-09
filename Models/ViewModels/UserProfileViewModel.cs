namespace TripGuide.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public User User { get; set; }
        public List<UserBlogPost> Collections { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
