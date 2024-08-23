using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Models;

namespace Mvc_Project.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole<int>> roleManager;

        public RoleController(RoleManager<IdentityRole<int>> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult AddRole()
        {
          
            
            return View("AddRole");
        }

        [HttpPost]
        public async Task<IActionResult> SaveRole(RoleViewModel roleModelFromReq)
        {
            if (ModelState.IsValid)
            {
                IdentityRole<int> Role = new IdentityRole<int>();

               Role.Name = roleModelFromReq.RoleName;

            var Resalt =   await roleManager.CreateAsync(Role);

                if (Resalt.Succeeded)
                {
                    ViewBag.sucess = true;  
                    return View("AddRole");


                }

                foreach (var item  in  Resalt.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }

            return View("AddRole", roleModelFromReq);

        }
    }
}
