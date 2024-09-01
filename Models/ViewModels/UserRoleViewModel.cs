namespace Mvc_Project.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<string> AvailableRoles { get; set; }
        public List<string> SelectedRoles { get; set; }

        public UserRoleViewModel()
        {
            AvailableRoles = new List<string>();
            SelectedRoles = new List<string>();
        }

    }
}
