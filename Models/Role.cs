namespace Mvc_Project.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}
