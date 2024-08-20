using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.Repositorys.Mvc_Project.Models.Repositorys;

namespace Mvc_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            
        }


        public IActionResult Index(string searchString)
        {
            searchString = String.IsNullOrEmpty(searchString)?"": searchString.ToLower();
            var products = _productRepository.GetAll();
            if(!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => (p.Name).ToLower().Contains(searchString)).ToList();
            }
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _productRepository.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Add(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
