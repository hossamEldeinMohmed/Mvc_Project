using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.Repositorys.Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.ViewModels;
using System.Collections.Generic;

namespace Mvc_Project.Controllers
{
    /* [Authorize(Roles = "admin")]*/
    public class AdminDashBordController : Controller
    {


        private readonly IProductRepository _productRepository;
        private readonly INotification notification;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly ICategory catagory;

        public AdminDashBordController(IProductRepository productRepository, INotification notification, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, ICategory catagory)
        {
            _productRepository = productRepository;
            this.notification = notification;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.catagory = catagory;
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
            var ProductNotification = new ProductNotificationViewModel();


            List<Product> Rejectedproducts = _productRepository.GetAllRejected();

            return View("ManageProducts", Rejectedproducts);
        }


        /*   public IActionResult RejectedProductes()
           {
               *//*int userId =*//*
               var rejectedProductsWithReasons = _productRepository.GetAllRejected()
                   .Select(product => new ProductNotificationViewModel
                   {
                       ProductId = product.Id,
                       ProductName = product.Name,
                       RejectionReason = product..FirstOrDefault()?.Message,
                       RejectionDate = product.Notifications.FirstOrDefault()?.CreatedAt ?? DateTime.Now
                   })
                   .ToList();

               return View("ManageProducts", rejectedProductsWithReasons);
           }

   */


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
            return View("AllAcceptedProductes", products);

        }








        public IActionResult AddCategory()
        {
            var viewModel = new CategoryViewModel
            {
                NewCategory = new Category()
            };

            return View("AddCategory", viewModel);
        }


        [HttpPost]
        public IActionResult CreateCategory(CategoryViewModel viewModel)
        {
          
            
               var existingCategory = catagory.GetByName(viewModel.NewCategory.Name);

            if (existingCategory != null)
            {
                ViewBag.Message = "Category already exists";
                ViewBag.MessageType = "error";
                return View("AddCategory", viewModel);
            }
            else
            {

                viewModel.NewCategory.Name = viewModel.NewCategory.Name.ToLower();
                catagory.Add(viewModel.NewCategory);

                ViewBag.Message = "Category added successfully";
                ViewBag.MessageType = "success";
                return View("AddCategory", viewModel);

            } /*if()
            {

                return View("AddCategory", viewModel);
            }*/
        }



    }
}
