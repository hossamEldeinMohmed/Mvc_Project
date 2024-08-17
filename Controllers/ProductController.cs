using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys; 

namespace Mvc_Project.Controllers
{
    public class ProductController : Controller
    {
        private  ProductRepository productRepository;

        public ProductController(ProductRepository _productRepository)
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
        public IActionResult Add(Product product)
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
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                //code to write
            }

            if (ModelState.IsValid)
            {
            
                    productRepository.Update(product);
               
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
