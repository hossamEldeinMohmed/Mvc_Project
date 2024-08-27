using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.ViewModels;
using System.Security.Claims;

namespace Mvc_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var users = _userRepository.GetAll();
            return View(users);
        }
        [Authorize]
        public IActionResult Details()
        {
            var userIdFromCookie = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdFromCookie))
            {
                return BadRequest("User is not authenticated or User ID is missing from the claim.");
            }

            if (!int.TryParse(userIdFromCookie, out int userIdFromCookieParsed))
            {
                return BadRequest("User ID is invalid.");
            }

            var user = _userRepository.GetByID(userIdFromCookieParsed);
            if (user == null)
            {
                return NotFound();
            }
            var viewModel = new UserProfileViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                EmailVerified = user.EmailVerified,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                ProfileImageUrl = user.ProfileImageUrl
            };


            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.Add(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserProfileViewModel viewModel)
        {
            if (id != viewModel.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var user = _userRepository.GetByID(id);
                if (user == null) return NotFound();

                user.UserName = viewModel.UserName;
                user.Email = viewModel.Email;
                user.PhoneNumber = viewModel.PhoneNumber;
                user.UpdatedAt = DateTime.UtcNow;

                if (viewModel.ProfileImageUrl != null)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.ProfileImage.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.ProfileImage.CopyToAsync(fileStream);
                    }

                    user.ProfileImageUrl = uniqueFileName;
                }

                _userRepository.Update(user);
                return RedirectToAction(nameof(Details));
            }

            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            var user = _userRepository.GetByID(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _userRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}


