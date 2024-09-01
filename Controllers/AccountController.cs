using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc_Project.Models;

namespace Mvc_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly IEmailSender emailSender;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<int>> roleManager,IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.emailSender = emailSender;
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
                    await SendConfirmationEmailAsync(userToDb, confirmationToken);


                    
                    return RedirectToAction("RegisterConfirmation", "Account");

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
        public async Task< IActionResult> SaveLogin(LoginUserViewModel UserLoginFromRequst)
        {

            if (ModelState.IsValid)
            {

             User UserFromDB =   await userManager.FindByEmailAsync(UserLoginFromRequst.Identifier);
                

                if (UserFromDB == null)
                {
                    UserFromDB = await userManager.FindByNameAsync(UserLoginFromRequst.Identifier);
                }
                if (UserFromDB != null)
                {
                  bool Found = await userManager.CheckPasswordAsync(UserFromDB, UserLoginFromRequst.Password);
                    if (Found)
                    {


                      await   signInManager.SignInAsync(UserFromDB, UserLoginFromRequst.RememberMe);

                        if (await userManager.IsInRoleAsync(UserFromDB, "Admin"))
                        {
                            
                            return RedirectToAction("Index", "AdminDashBord");
                        }

                        return RedirectToAction("Index", "Home");
                    }

                }

                ModelState.AddModelError("", "User Email / User Name or Password Wrong");

            }
            return View("Login",UserLoginFromRequst);
        }


        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");

        }

            public async Task<IActionResult>ConfirmEmail(string userId, string token)
        {
            if(userId == null || token == null)
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
    <p>Thank you for registering with She Shares, a community dedicated to empowering women.</p>
    <p>Please confirm your email address by clicking the button below:</p>
    <p style='text-align:center;'>
        <a href='{confirmationLink}' style='background-color:#de7c7c;color:white;padding:10px 20px;text-decoration:none;border-radius:5px;font-weight:bold;display:inline-block;'>Confirm your email</a>
    </p>
    
    <p>Best regards,<br/>She Shares Team</p>";


            await emailSender.SendEmailAsync(user.Email, emailSubject, emailBody);

        }

        }
    }
