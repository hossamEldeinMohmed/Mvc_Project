using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.Repositorys.Mvc_Project.Models.Repositorys;

namespace Mvc_Project.Controllers
{
   /* [Authorize(Roles = "admin")]*/
    public class AdminDashBordController : Controller
    {
        

        private readonly IProductRepository _productRepository;
        private readonly INotification notification;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly ICrudOperation<Category> catagory;

        public AdminDashBordController(IProductRepository productRepository, INotification notification, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager  /*ICrudOperation<Category> catagory*/)
        {
            _productRepository = productRepository;
            this.notification = notification;
            this.userManager = userManager;
            this.roleManager = roleManager;
          /*  this.catagory = catagory;*/
        }



        public IActionResult Index()
        {
            return View("Index");
        }


        public IActionResult ManageProducts()
        {
            
            var products = _productRepository.GetAllWithUser();
            return View(products);
        }

        [HttpPost]
        public IActionResult UpdateProductStatus(int productId, string status, string rejectReason)
        {
            var product = _productRepository.GetByID(productId);

            if (product != null)
            {
                product.Status = status;

                if (status == "Rejected")
                {
                    var rejectionMessage = $"Your product '{product.Name}' has been rejected. Reason: {rejectReason}";

                    
                    notification.AddNotification(product.UserId, rejectionMessage);
                }

                _productRepository.Update(product);
            }

            return RedirectToAction("ManageProducts");
        }

        public IActionResult RejectedProductes()
        {
         List<Product> Rejectedproducts = _productRepository.GetAllRejected();

            return View ("ManageProducts",Rejectedproducts);
        }



        public IActionResult AllAccptedProductes()
        {
            List<Product> Allproducts = _productRepository.GetAll();

           /* return View("AllAcceptedProductes", Allproducts);*/

            return View("/Views/AdminDashBord/AllAcceptedProductes.cshtml", Allproducts);
        }

        public IActionResult MakeNotificationAsRead(int NotificationId)
        {
            notification.MarkAsRead(NotificationId);
            return View();

        }

        public ActionResult ProductList(string searchQuery)
        {
           

            ViewBag.SearchQuery = searchQuery;
            if (ViewBag.SearchQuery == null)
            {
                var productss = _productRepository.GetAll();
                return View("AllAcceptedProductes", productss);
            }

           List<Product> products = _productRepository.SearchAdvancedProducts(searchQuery);

            foreach (var product in products)
            {
                Console.WriteLine($"Product: {product.Name}, UserName: {product.User?.UserName}");

            }
            return View("AllAcceptedProductes",products);

        }





/*
        public IActionResult AllCategories()
        {
            var categories = catagory.GetAll();
            return View("categories", categories);
        }



        [HttpPost]
        public IActionResult CreateCategory(Category categoryfromAdmin)
        {
            if (ModelState.IsValid)
            {
                categoryfromAdmin.Name.ToLower();
                
                catagory.Add(categoryfromAdmin);
                return View("categories");
            }
            return View("categories", categoryfromAdmin);
        }

*/
    }
}
