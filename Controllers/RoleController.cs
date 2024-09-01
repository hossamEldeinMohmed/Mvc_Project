using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc_Project.Models;
using Mvc_Project.Models.ViewModels;
namespace Mvc_Project.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly UserManager<User> userManager;
        private readonly ILogger<Role> logger;

        public RoleController(RoleManager<IdentityRole<int>> roleManager, UserManager<User> userManager, ILogger<Role> logger)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
        }
        public IActionResult AddRole()
        {

            var model = new RoleViewModel()
            {
                RoleName = string.Empty
            };
            return View("AddRole", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveRole(RoleViewModel roleModelFromReq)
        {
            if (ModelState.IsValid)
            {

                string roleNameLowerCase = roleModelFromReq.RoleName.Trim().ToLower();


                var existingRole = await roleManager.FindByNameAsync(roleNameLowerCase);
                if (existingRole != null)
                {
                    ModelState.AddModelError("", "Role already exists.");
                    return View("AddRole", roleModelFromReq);
                }


                IdentityRole<int> role = new IdentityRole<int>
                {
                    Name = roleNameLowerCase
                };

                var result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                {

                    ViewBag.Success = "Role created successfully!";
                    return View("AddRole");
                }


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("AddRole", roleModelFromReq);
        }







        public async Task<IActionResult> AssignRoles()
        {
            var users = await userManager.Users.ToListAsync();
            var roles = await roleManager.Roles.Select(r => r.Name).ToListAsync();

            var model = new UserRoleAssignmentViewModel
            {
                Users = (await Task.WhenAll(users.Select(async u => new UserRoleViewModel
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    AvailableRoles = roles,
                    SelectedRoles = (await userManager.GetRolesAsync(u)).ToList()
                }))).ToList()
            };

            return View("AssignRoles", model);
        }




       
 





        [HttpPost]
        public async Task<IActionResult> AssignRoles(UserRoleAssignmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AssignRoles", model);
            }

            var success = true;

            foreach (var userModel in model.Users)
            {
                try
                {
                    var user = await userManager.FindByIdAsync(userModel.UserId.ToString());
                    if (user == null)
                    {
                        ModelState.AddModelError("", $"User with ID {userModel.UserId} not found.");
                        success = false;
                        continue;
                    }

                    var currentRoles = await userManager.GetRolesAsync(user);
                    var rolesToAdd = userModel.SelectedRoles.Except(currentRoles).ToList();
                    var rolesToRemove = currentRoles.Except(userModel.SelectedRoles).ToList();

                    if (rolesToAdd.Any())
                    {
                        var addResult = await userManager.AddToRolesAsync(user, rolesToAdd);
                        if (!addResult.Succeeded)
                        {
                            logger.LogError($"Failed to add roles {string.Join(", ", rolesToAdd)} to user {userModel.UserName}");
                            ModelState.AddModelError("", $"An error occurred while adding roles {string.Join(", ", rolesToAdd)} to user {userModel.UserName}.");
                            success = false;
                        }
                        else
                        {
                            TempData["SuccessMessage"] = $"Successfully added roles {string.Join(", ", rolesToAdd)} to user {userModel.UserName}.";
                        }
                    }

                    if (rolesToRemove.Any())
                    {
                        var removeResult = await userManager.RemoveFromRolesAsync(user, rolesToRemove);
                        if (!removeResult.Succeeded)
                        {
                            logger.LogError($"Failed to remove roles {string.Join(", ", rolesToRemove)} from user {userModel.UserName}");
                            ModelState.AddModelError("", $"An error occurred while removing roles {string.Join(", ", rolesToRemove)} from user {userModel.UserName}.");
                            success = false;
                        }
                        else
                        {
                            TempData["SuccessMessage"] = $"Successfully removed roles {string.Join(", ", rolesToRemove)} from user {userModel.UserName}.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while assigning roles");
                    ModelState.AddModelError("", $"An unexpected error occurred: {ex.Message}");
                    success = false;
                }
            }

            if (success)
            {
                TempData["SuccessMessage"] = "Roles assigned successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Some roles could not be assigned. Please check the details.";
            }

            return RedirectToAction("AssignRoles");
        }

    }
}