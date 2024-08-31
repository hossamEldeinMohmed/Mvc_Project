namespace Mvc_Project.Models.ViewModels
{
    public class UserRoleAssignmentViewModel
    {
        public List<UserRoleViewModel> Users { get; set; }
        public UserRoleAssignmentViewModel()
        {
            Users = new List<UserRoleViewModel>();
        }
    }
}
