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
            var products = _productRepository.GetAll().Where(p => p.CategoryId == id).Take(12).ToList();

            return View("Dresses", products);

        }
        public IActionResult Tops(int id = 2)
        {
            var products = _productRepository.GetAll().Where(p => p.CategoryId == id).Take(12).ToList();

            return View("Tops", products);

        }
        public IActionResult Bottoms(int id = 3)
        {
            var products = _productRepository.GetAll().Where(p => p.CategoryId == id).Take(12).ToList();

            return View("Bottoms", products);

        }
        public IActionResult Shoes(int id = 4)
        {
            var products = _productRepository.GetAll().Where(p => p.CategoryId == id).Take(12).ToList();

            return View("Shoes", products);

        }
        public IActionResult AccessoriesAndMakeUp(int id = 5)
        {
            var products = _productRepository.GetAll().Where(p => p.CategoryId == id).Take(12).ToList();

            return View("AccessoriesAndMakeUp", products);

        }

    }

}

//using Microsoft.AspNetCore.Mvc;
//using Mvc_Project.Models;
//using Mvc_Project.Models.Repositorys.Mvc_Project.Models.Repositorys;
//using Mvc_Project.Models.ViewModels;

//namespace Mvc_Project.Controllers
//{
//    public class CategoryController : Controller
//    {
//        private readonly IProductRepository _productRepository;

//        public CategoryController(IProductRepository productRepository)
//        {
//            _productRepository = productRepository;

//        }

//        private IActionResult GetProductsByCategoryName(string categoryName, string viewName)
//        {
//            var category = _productRepository.GetAll()
//                                .FirstOrDefault(p => p.Category.Name == categoryName)?.Category;

//            if (category == null)
//            {
//                return NotFound();
//            }

//            var products = _productRepository.GetAll()
//                                .Where(p => p.CategoryId == category.Id)
//                                .Take(12)
//                                .ToList();

//            return View(viewName, products);
//        }

//        public IActionResult Dresses()
//        {
//            return GetProductsByCategoryName("Dresses", "Dresses");
//        }

//        public IActionResult Tops()
//        {
//            return GetProductsByCategoryName("Tops", "Tops");
//        }

//        public IActionResult Bottoms()
//        {
//            return GetProductsByCategoryName("Bottoms", "Bottoms");
//        }

//        public IActionResult Shoes()
//        {
//            return GetProductsByCategoryName("Shoes", "Shoes");
//        }

//        public IActionResult AccessoriesAndMakeUp()
//        {
//            return GetProductsByCategoryName("Accessories and MakeUp", "AccessoriesAndMakeUp");
//        }
//    }
//}
