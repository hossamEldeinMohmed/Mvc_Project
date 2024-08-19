using System.ComponentModel.DataAnnotations;

namespace Mvc_Project.Models
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        public string Email { get; set; }


        [Compare("Email")]
        [Display(Name = "Confirm Email")]
        public string ConfirmedEmail { get; set; }

        public string PhoneNumber { get; set; }

        public bool RememberMe { get; set; }


    }
}
