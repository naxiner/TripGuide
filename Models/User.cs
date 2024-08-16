using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripGuide.Models
{
    public class User : IdentityUser
    {
        [NotMapped]
        public IFormFile? AvatarImage { get; set; }
        public string? AvatarImageUrl { get; set; }
    }
}