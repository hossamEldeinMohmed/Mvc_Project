using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.Repositorys.Mvc_Project.Models.Repositorys;
using Mvc_Project.Models.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace Mvc_Project.Controllers
{
    [Authorize(Roles = "admin")]
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
                else if (status == "Accepted")
                {
                    var acceptanceMessage = $" congratulations!  Your product '{product.Name}' has been accepted.";
                    notification.AddNotification(product.UserId, acceptanceMessage);
                }

                _productRepository.Update(product);
            }

            return RedirectToAction("ManageProducts");
        }


        public IActionResult RejectedProductes()
        {
            List<Product> Rejectedproducts = _productRepository.GetAllRejected();

            return View("ManageProducts", Rejectedproducts);
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






        private CategoryViewModel PrepareCategoryViewData()
        {
            List<Category> categories = catagory.GetAll();

            var productCounts = new Dictionary<int, int>();

            foreach (var category in categories)
            {
                productCounts[category.Id] = _productRepository.GetProductCountByCategory(category.Id);
            }

            return new CategoryViewModel
            {
                Categories = categories,
                ProductCounts = productCounts
            };
        }



        public IActionResult ShowAllCategories()
        {
            var GetCatgorydata = PrepareCategoryViewData();


            return View("categories", GetCatgorydata);

        }




        public IActionResult DeleteCategory(int id)
        {
            var category = catagory.GetByID(id);

            if (category == null)
            {
                var CatagoryviewModel = PrepareCategoryViewData();
                ViewBag.Message = "Category not found";
                ViewBag.MessageType = "error";

                return View("categories", CatagoryviewModel);
            }

            catagory.Delete(id);

            var updatedViewModel = PrepareCategoryViewData();
            ViewBag.Message = "Category deleted successfully";
            ViewBag.MessageType = "success";

            return View("categories", updatedViewModel);
        }





        /* [HttpGet]
         public IActionResult GetNotifications()
         {
             var userIdFromCookie = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

             if (int.TryParse(userIdFromCookie, out int userIdFromCookieParth))
             {
                 var notifications = notification.GetUserNotifications(userIdFromCookieParth);

                 return Json(new
                 {
                     NotificationCount = notifications.Count(n => !n.IsRead),
                     Notifications = notifications.Select(n => new
                     {
                         n.Id,
                         n.Message,
                         n.CreatedAt,
                         n.IsRead
                     }).ToList()
                 });
             }

             return BadRequest("Invalid user ID.");
         }
 */



        [AllowAnonymous]
        public IActionResult GetNotifications()
        {
            var userIdFromCookie = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"Extracted User ID from Cookie: {userIdFromCookie}");

            if (int.TryParse(userIdFromCookie, out int userIdFromCookieParth))
            {
                Console.WriteLine($"Parsed User ID: {userIdFromCookieParth}");
                var notifications = notification.GetUserNotifications(userIdFromCookieParth);

                return Json(new
                {
                    NotificationCount = notifications.Count(n => !n.IsRead),
                    Notifications = notifications.Select(n => new
                    {
                        n.Id,
                        n.Message,
                        n.CreatedAt,
                        n.IsRead
                    }).ToList()
                });
            }

            return BadRequest("Invalid user ID.");
        }

        [AllowAnonymous]

        [HttpPost]
        public IActionResult MarkAsRead([FromBody] List<int> notificationIds)
        {
            if (notificationIds != null && notificationIds.Any())
            {
                foreach (var notificationId in notificationIds)
                {
                    notification.MarkAsRead(notificationId);
                }

                var userIdFromCookie = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdFromCookie, out int userIdFromCookieParth))
                {
                    var notifications = notification.GetUserNotifications(userIdFromCookieParth);
                    var unreadCount = notifications.Count(n => !n.IsRead);

                    return Json(new
                    {
                        NotificationCount = unreadCount
                    });
                }
            }

            return BadRequest("Invalid notification IDs.");
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetReadNotifications()
        {
            var userIdFromCookie = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdFromCookie, out int userIdFromCookieParth))
            {
                var notifications = notification.GetReadNotifications(userIdFromCookieParth);

                return Json(new
                {
                    Notifications = notifications.Select(n => new
                    {
                        n.Id,
                        n.Message,
                        n.CreatedAt,
                        n.IsRead
                    }).ToList()
                });
            }

            return BadRequest("Invalid user ID.");

        }


        public async Task<IActionResult> Statistics()
        {
            var statisticsModel = new CountViewModel
            {
                ProductCount = _productRepository.GetAll().Count(),
                PendingProduct = _productRepository.GetAllWithUser().Count(),
                UserCount = await userManager.Users.CountAsync()
            };

            
            return Json(statisticsModel);
        }


    }

    }


