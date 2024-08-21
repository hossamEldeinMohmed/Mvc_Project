using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys.Mvc_Project.Models.Repositorys;
using System.Diagnostics;

namespace Mvc_Project.Controllers
{
    public class HomeController : Controller
    {
        IProductRepository _productRepository;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger , IProductRepository productRepo)
        {
            _logger = logger;
            _productRepository = productRepo;
        }


      
        public IActionResult Index()
        {

            var AllProducts = _productRepository.GetAllRandomly();
            return View(AllProducts);
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
