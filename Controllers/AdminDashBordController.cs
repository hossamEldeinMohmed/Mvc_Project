using Microsoft.AspNetCore.Mvc;

namespace Mvc_Project.Controllers
{
    public class AdminDashBordController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
