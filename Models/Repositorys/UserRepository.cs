namespace Mvc_Project.Models.Repositorys
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    namespace Mvc_Project.Models.Repositorys
    {
        public class UserRepository : IUserRepository
        {
            private  Context _context;

            public UserRepository(Context context)
            {
                _context = context;
            }

            public List<User> GetAll()
            {
                return _context.Users.ToList();
            }

            public User GetByID(int id)
            {
                return _context.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefault(u => u.Id == id);
            }
            public User GetByName(string name)
            {
                return _context.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefault(u => u.UserName == name);
            }

            public void Add(User newUser)
            {
                _context.Users.Add(newUser);
                _context.SaveChanges();
            }

            public void Update(User updatedUser)
            {
                _context.Users.Update(updatedUser);
                _context.SaveChanges();
            }

            public void Delete(int id)
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
            }

            public User GetByEmail(string email)
            {
                return _context.Users
                    .FirstOrDefault(u => u.Email == email);
            }

            public List<User> GetUsersByRole(int roleId)
            {
                return _context.Users
                    .Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId))
                    .ToList();
            }
        }
    }

}
