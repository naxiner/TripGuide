namespace TripGuide.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile? AvatarImage { get; set; }
        public string? AvatarImageUrl { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
        public IList<string> AllRoles { get; set; } = new List<string>();
    }
}
