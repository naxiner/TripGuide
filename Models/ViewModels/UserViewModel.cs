namespace TripGuide.Models.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<UserRolesViewModel> Users { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
