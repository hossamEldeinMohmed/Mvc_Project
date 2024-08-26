using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.Repositorys.Mvc_Project.Models.Repositorys;

namespace Mvc_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly IEmailSender emailSender;
        private readonly IUserRepository _userRepository;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<int>> roleManager, IEmailSender emailSender, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.emailSender = emailSender;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterUserViewModel DataFromRequst)
        {
            if (ModelState.IsValid)
            {

                // Ensure the roles are created
                await CreateRole("vendor");

                var existingUserName = await userManager.FindByNameAsync(DataFromRequst.UserName);
                if (existingUserName != null)
                {
                    ModelState.AddModelError("", "Username already exists.");
                    return View("Register", DataFromRequst);
                }


                var existingEmail = await userManager.FindByEmailAsync(DataFromRequst.Email);
                if (existingEmail != null)
                {
                    ModelState.AddModelError("", "Email already exists.");
                    return View("Register", DataFromRequst);
                }

                var existingPhoneNumber = await userManager.Users
                    .AnyAsync(u => u.PhoneNumber == DataFromRequst.PhoneNumber);
                if (existingPhoneNumber)
                {
                    ModelState.AddModelError("", "Phone number already exists.");
                    return View("Register", DataFromRequst);
                }
                User userToDb = new User();

                userToDb.UserName = DataFromRequst.UserName;
                userToDb.PasswordHash = DataFromRequst.Password;
                userToDb.Email = DataFromRequst.Email;
                userToDb.PhoneNumber = DataFromRequst.PhoneNumber;
                userToDb.CreatedAt = DateTime.UtcNow;

                //saving in DB
                //passward Hasing
                var Resalt = await userManager.CreateAsync(userToDb, DataFromRequst.Password);

                if (Resalt.Succeeded)
                {
                    await userManager.AddToRoleAsync(userToDb, "vendor");

                    var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(userToDb);
                    //await SendConfirmationEmailAsync(userToDb, confirmationToken);


                    /* //make cookie

                     await signInManager.SignInAsync(userToDb, false);*/

                    return RedirectToAction("Login", "Account");

                }

                foreach (var item in Resalt.Errors)
                {

                    ModelState.AddModelError("", item.Description);
                }
            }


            return View("Register", DataFromRequst);

        }


        public IActionResult Login()
        {
            return View("Login");

        }

        [HttpPost]
        public async Task<IActionResult> SaveLogin(LoginUserViewModel UserLoginFromRequst)
        {

            if (ModelState.IsValid)
            {

                User UserFromDB = await userManager.FindByEmailAsync(UserLoginFromRequst.Identifier);


                if (UserFromDB == null)
                {
                    UserFromDB = await userManager.FindByNameAsync(UserLoginFromRequst.Identifier);
                }
                if (UserFromDB != null)
                {
                    bool found = await userManager.CheckPasswordAsync(UserFromDB, UserLoginFromRequst.Password);
                    if (found)
                    {
                        UserFromDB.LastLoginDate = DateTime.Now;
                        _userRepository.Update(UserFromDB);
                        await signInManager.SignInAsync(UserFromDB, UserLoginFromRequst.RememberMe);
                        return LocalRedirect("/User");
                    }

                }

                ModelState.AddModelError("", "User Email / User Name or Password Wrong");

            }
            return View("Login", UserLoginFromRequst);
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return View("Error");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ViewBag.IsEmailConfirmed = true;


                ViewBag.RedirectUrl = Url.Action("Login", "Account");

                return View("Confirmation");
            }

            ViewBag.IsEmailConfirmed = false;
            return View("Confirmation");
        }

        public async Task<IActionResult> ConfirmOrResendEmail(string userId, string token = null)
        {
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            if (token != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    ViewBag.IsEmailConfirmed = true;
                    return View("Confirmation");
                }
                else
                {
                    ViewBag.IsEmailConfirmed = false;
                    return View("Confirmation");
                }
            }
            else
            {
                var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                await SendConfirmationEmailAsync(user, confirmationToken);

                ViewBag.UserId = user.Id;
                ViewBag.ResendLink = "A new confirmation email has been sent. Please check your inbox.";

                return View("Confirmation");
            }
        }


        public IActionResult RegisterConfirmation(string userId)
        {
            ViewBag.UserId = userId;
            return View("Confirmation");
        }

        private async Task SendConfirmationEmailAsync(User user, string token)
        {
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

            var emailSubject = "Please Confirm Your Email";
            var emailBody = $@"
        <h2>Hello {user.UserName},</h2>
        <p>Thank you for registering with us. Please confirm your email address by clicking the link below:</p>
        <p><a href='{confirmationLink}'>Confirm your email</a></p>
        <p>If you did not create an account, no further action is required.</p>
        <p>Best regards,<br/> Cloth Carousel Team</p>";

            await emailSender.SendEmailAsync(user.Email, emailSubject, emailBody);
        }

        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    var result = await roleManager.CreateAsync(new IdentityRole<int> { Name = roleName });
                    if (result.Succeeded)
                    {
                        return Ok("Role created successfully");
                    }
                    else
                    {
                        return BadRequest("Error creating role");
                    }
                }
                else
                {
                    return BadRequest("Role already exists");
                }
            }
            return BadRequest("Role name is required");
        }
    }



}
