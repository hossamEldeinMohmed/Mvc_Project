using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.Repositorys.Mvc_Project.Models.Repositorys;
using System.Diagnostics;
using System.Security.Claims;

namespace Mvc_Project.Controllers
{
    public class HomeController : Controller
    {
        IProductRepository _productRepository;
        IUserRepository _UserRepository;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger , IProductRepository productRepo, IUserRepository userRepo)
        {
            _logger = logger;
            _productRepository = productRepo;
            _UserRepository = userRepo;
        }


      
        public IActionResult Index(int pageNumber = 1)
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

            var user = _UserRepository.GetByID(userIdFromCookieParsed);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.UserName = user.UserName;
            ViewBag.ProfileImageUrl = user.ProfileImageUrl;
            int pageSize = 12;
            var totalProducts = _productRepository.Count();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
            var AllProducts = _productRepository.GetAllRandomly( pageNumber, pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            return View(AllProducts);
        }
        public IActionResult Search(string searchString)
        {
            searchString = String.IsNullOrEmpty(searchString) ? "" : searchString.ToLower();
            var products = _productRepository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => (p.Name).ToLower().Contains(searchString)).ToList();
            }
            return View("Index",products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
