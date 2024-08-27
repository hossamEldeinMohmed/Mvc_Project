using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.Repositorys.Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.ViewModels;
using System.Security.Claims;

namespace Mvc_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string searchString)
        {
            searchString = string.IsNullOrEmpty(searchString) ? "" : searchString.ToLower();
            var products = _productRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.ToLower().Contains(searchString)).ToList();
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

        [Authorize]
        public IActionResult Add()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userIdFromCookie = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int userIdFromCookieParth = int.Parse(userIdFromCookie);

                // Initialize the ProductImages list if it's null
                if (viewModel.ProductImages == null)
                {
                    viewModel.ProductImages = new List<ProductImages>();
                }

                if (viewModel.ProductImageFormFile != null && viewModel.ProductImageFormFile.Any())
                {
                    UploadFiles(viewModel);
                }

                var product = new Product
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    CategoryId = viewModel.CategoryId ?? 0,
                    Price = viewModel.Price,
                    ProductImges = viewModel.ProductImages,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = userIdFromCookieParth
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

        private bool UploadFiles(ProductFormViewModel model)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "ProductFile");

            // Create folder if it doesn't exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            foreach (var formFile in model.ProductImageFormFile)
            {
                var fileName = ChangeFileName(formFile.FileName, model.Name);
                var fileNameWithPath = Path.Combine(path, fileName);

                using var stream = new FileStream(fileNameWithPath, FileMode.Create);
                formFile.CopyTo(stream);

                var productImage = new ProductImages { FileName = fileName };
                model.ProductImages.Add(productImage);
            }

            return true;
        }

        public static string ChangeFileName(string filename, string productName)
        {
            string[] substring = filename.Split('.');
            string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            return $"{productName}_{substring[0]}_{dateTime}.{substring[1]}";
        }

        [Authorize]
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
        [Authorize]
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

        [Authorize]
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
