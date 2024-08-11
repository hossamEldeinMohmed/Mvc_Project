using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys; 

namespace Mvc_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

    
        public IActionResult Index()
        {
            var products = productRepository.GetAll();
            return View(products);
        }

        
        public IActionResult Details(int id)
        {
            var product = productRepository.GetByID(id);

            return View(product);
        }

       
        public IActionResult Add()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Proudect product)
        {
            if (ModelState.IsValid)
            {
                productRepository.Add(product);
                return RedirectToAction("Index"); 
            }
            return View(product);
        }

       
        public IActionResult Edit(int id)
        {
            var product = productRepository.GetByID(id);
           
            return View(product);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Proudect product)
        {
            if (id != product.Id)
            {
                return BadRequest(); 
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productRepository.Update(product);
                }
                catch (Exception ex)
                {
                   
                    return View(product);
                }
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
