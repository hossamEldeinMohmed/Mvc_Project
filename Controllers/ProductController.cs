using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.Repositorys.Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.ViewModels;

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

           
            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryName = product.Category?.Name, 
                ProductImages = product.ProductImges 
            };

            return View(viewModel);
        }


        public IActionResult Add()
        {
            var categories = _productRepository.GetAllCategories();
            var categorySelectList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            var viewModel = new ProductViewModel
            {
                CategoryList = categorySelectList
            };

            return View(viewModel);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Initialize the ProductImages list if it's null
                if (viewModel.ProductImages == null)
                {
                    viewModel.ProductImages = new List<string>();
                }

                if (Request.Form.Files.Count > 0)
                {
                    foreach (var file in Request.Form.Files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                            if (!Directory.Exists(uploadsFolder))
                            {
                                Directory.CreateDirectory(uploadsFolder);
                            }

                            var filePath = Path.Combine(uploadsFolder, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            viewModel.ProductImages.Add(fileName);
                        }
                    }
                }

                var product = new Product
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    CategoryId = viewModel.CategoryId ?? 0,
                    Price = viewModel.Price,
                    ProductImges = viewModel.ProductImages,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _productRepository.Add(product);

                return RedirectToAction("Index");
            }

            var categories = _productRepository.GetAllCategories();
            ViewBag.CategoryList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View(viewModel);
        }




        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = _productRepository.GetAllCategories();
            ViewBag.CategoryList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

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
