using Microsoft.AspNetCore.Identity;

namespace Mvc_Project.Models.Repositorys
{
    public class RoleReprository 
    {
     
            private readonly Context _context;

            public RoleReprository(Context context)
            {
                _context = context;
            }

        public List<IdentityRole<int>> GetAll()
        {
            return _context.Roles.ToList(); 
        }

        public IdentityRole<int> GetByID(int id) 
        {
            return _context.Roles.Find(id);
        }

        public void Add(IdentityRole<int> newRole)
        {
            _context.Roles.Add(newRole);
            _context.SaveChanges();
        }

        public void Update(IdentityRole<int> updatedRole)
        {
            _context.Roles.Update(updatedRole);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var role = _context.Roles.Find(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }
        }
    }
