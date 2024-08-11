using Microsoft.EntityFrameworkCore;

namespace Mvc_Project.Models
{
    public class Context: DbContext
    {
        public Context() : base() { }
        public DbSet<Proudect> Proudects { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { 
        
        }
    }
}
