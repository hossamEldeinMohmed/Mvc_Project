using Microsoft.AspNetCore.Identity;

namespace Mvc_Project.Models
{
    public class Role : IdentityRole<int>
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }

}
