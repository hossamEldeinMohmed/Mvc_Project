using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Models;

namespace Mvc_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterUserViewModel DataFromRequst) {
            if (ModelState.IsValid)
            {
                User userToDb = new User();

                userToDb.UserName = DataFromRequst.UserName;
                userToDb.PasswordHash = DataFromRequst.Password;
                userToDb.Email = DataFromRequst.Email;
                userToDb.PhoneNumber = DataFromRequst.PhoneNumber;

                //saving in DB
                var Resalt = await userManager.CreateAsync(userToDb);

                if (Resalt.Succeeded && DataFromRequst.RememberMe)
                {
                    //make cookie

                 await signInManager.SignInAsync(userToDb,false);

                    return RedirectToAction("Register");

                }

                foreach (var item in Resalt.Errors)
                {

                    ModelState.AddModelError("",item.Description);
                }
            }

            return View("Register" , DataFromRequst);

            }
    }
}
