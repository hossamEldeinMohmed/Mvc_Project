using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;

            _webHostEnvironment = webHostEnvironment;

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
        public async Task<IActionResult> Add(ProductFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Initialize the ProductImages list if it's null
                if (viewModel.ProductImages != null)
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

        private bool UploadFiles(ProductFormViewModel  model)
        {


            string path = Path.Combine(_webHostEnvironment.WebRootPath, "ProductFile");

            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);



            var fileName = String.Empty;
            var fileNameWithPath = String.Empty;


            foreach (var formFile in model.ProductImageFormFile)
            {
                fileName = ChangeFileName(formFile.FileName, model.Name);

                fileNameWithPath = Path.Combine(path, fileName);

                using var stream = new FileStream(fileNameWithPath, FileMode.Create);
                formFile.CopyTo(stream);
                var productImages = new ProductImages {FileName=fileName};
                model.ProductImages.Add(productImages);
            }


            return true;
        }


        public static string ChangeFileName(string filename, string productName)

        {
            string newFileName;

            string[] substring;

            char[] delimitdot = { '.' };

            char[] delimitspace = { ' ' };

            substring = filename.Split(delimitdot, 2);

            string dateTimenew = DateTime.Now.ToString().Replace('/', ' ');

            string dateTime = dateTimenew.Replace(':', ' ');

            string[] substring1 = dateTime.Split(delimitspace);

            string appenddatetime = "";

            int i;

            for (i = 0; i < substring1.Length - 1; i++)

            {
                appenddatetime += substring1[i];
            }

            return $"{productName}_{substring[0]}{appenddatetime}.{substring[1]}";
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
