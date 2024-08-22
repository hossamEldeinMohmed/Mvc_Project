using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Models.Repositorys.Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.ViewModels;

namespace Mvc_Project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CategoryController(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }

        public IActionResult Dresses(int id = 1)
        {
            var products = _productRepository.GetAll().Where(p => p.CategoryId == id).ToList();
            
                return View("Dresses", products);

        }
        public IActionResult Tops(int id = 2)
        {
            var products = _productRepository.GetAll().Where(p => p.CategoryId == id).ToList();

            return View("Tops", products);

        }
        public IActionResult Bottoms(int id = 3)
        {
            var products = _productRepository.GetAll().Where(p => p.CategoryId == id).ToList();

            return View("Bottoms", products);

        }
        public IActionResult Shoes(int id = 4)
        {
            var products = _productRepository.GetAll().Where(p => p.CategoryId == id).ToList();

            return View("Shoes", products);

        }
        public IActionResult AccessoriesAndMakeUp(int id = 5)
        {
            var products = _productRepository.GetAll().Where(p => p.CategoryId == id).ToList();

            return View("AccessoriesAndMakeUp", products);

        }

    }

}
