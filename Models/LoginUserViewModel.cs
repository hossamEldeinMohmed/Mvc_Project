using System.ComponentModel.DataAnnotations;

namespace Mvc_Project.Models
{
    public class LoginUserViewModel
    {


        [Display(Name = "User Name Or Email")]

        [Required(ErrorMessage = "Email or Username is required")]
      public string Identifier { get; set; } 

      [Required(ErrorMessage = "Password is required")]
      [DataType(DataType.Password)]
      public string Password { get; set; }



      [Display(Name = "Remamber Me")]
      public bool RememberMe { get; set; }
        



    }
}
