namespace Mvc_Project.Models.ViewModels
{
    public class UserProfileViewModel
    {

        public int Id { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IFormFile ProfileImage { get; set; }


    }
}
