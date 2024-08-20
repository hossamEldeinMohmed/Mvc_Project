using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Models.ViewModels;

namespace Mvc_Project.Controllers
{
    public class CatgoryController : Controller
    {

        public IActionResult showCatgory(int id)
        {
            if(id == 1)
            {
                return View("Dresses");

            }
            if (id == 2)
            {
                return View("Tops");

            }
            if (id == 3)
            {
                return View("Bottoms");

            }
            if (id == 4)
            {
                return View("Shoes");

            }
            if (id == 5)
            {
                return View("AccessoriesAndMakeUp");

            }
            return View("Index");

        }
    }
}
