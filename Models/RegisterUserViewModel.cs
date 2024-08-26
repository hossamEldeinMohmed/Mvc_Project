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


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Compare("Email")]
        [Display(Name = "Confirm Email")]
        public string ConfirmedEmail { get; set; }

        [RegularExpression("^(\\+201|01|00201)[0-2,5]{1}[0-9]{8}",ErrorMessage ="It Must Be Egyptain Number With 11 Number") ]
        public string PhoneNumber { get; set; }

       


    }
}
