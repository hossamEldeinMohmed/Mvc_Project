using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mvc_Project.Models.Repositorys.Mvc_Project.Models.Repositorys;

namespace Mvc_Project.Models.Repositorys
{
    public class ProductRepository : IProductRepository
    {
        private  Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products
                .Where(p => p.Status != "Pending" && p.Status != "Rejected")
                .Include(p => p.Category)
                .Include(p => p.User)
                .Include(p => p.ProductAttributes)
                .Include(p => p.ProductReviews)
                .ToList();
        }

        public List<Product> GetAllRandomly(int pageNumber, int pageSize)
        {
            return _context.Products
                .Where(p => p.Status != "Pending" && p.Status != "Rejected")
                .Include(p => p.Category)                
                .Include(p => p.ProductAttributes)
                .Include(p => p.ProductReviews)
                .OrderBy(p => Guid.NewGuid()).Skip((pageNumber - 1) * pageSize).Take(pageSize)

                
                .OrderBy(p => Guid.NewGuid())  // Randomly sort the products
                .ToList();
        }
        public Product GetByID(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductAttributes)
                .Include(p => p.ProductReviews)
                
                .FirstOrDefault(p => p.Id == id);
        }

         
        //public List<Product> showCategory(int id)
        // {
        //     return _context.Products
        //          .Include(p => p.Category)
        //          .Include(p => p.ProductAttributes)
        //          .Include(p => p.ProductReviews)
        //          .Where(p => p.CategoryId == id)  // filtered by category id
        //          .ToList();
        // }

        public void Add(Product newProduct)
        {
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        public void Update(Product updatedProduct)
        {
            _context.Products.Update(updatedProduct);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var Product = _context.Products.Find(id);
            if (Product != null)
            {
                _context.Products.Remove(Product);
                _context.SaveChanges();
            }
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .ToList();
        }
       


        public List<Product> SearchProducts(string searchTerm)
        {
            return _context.Products
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .Include(p => p.Category)
                .ToList();
        }
        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public List<Product> GetAllRejected()
        {
            return _context.Products
                .Where(p => p.Status != "Accepted" && p.Status != "Pending")
                .Include(p => p.User)
                .Include(p => p.Category)

                .ToList();
        }

        public List<Product> SearchAdvancedProducts(string searchTerm)
        {
          
            var lowerSearchTerm = searchTerm.ToLower();



            return _context.Products
          .Where(p => p.Status != "Pending" && p.Status != "Rejected")
         .Where(p => p.Name != null && p.Name.ToLower().Contains(lowerSearchTerm) ||
                     p.Description != null && p.Description.ToLower().Contains(lowerSearchTerm) ||
                     p.Category != null && p.Category.Name != null && p.Category.Name.ToLower().Contains(lowerSearchTerm) ||
                     p.User != null && p.User.UserName != null && p.User.UserName.ToLower().Contains(lowerSearchTerm))
         .Include(p => p.Category)
         
         .Include(p => p.User)
         .ToList();
        }

        public List<Product> GetAllRandomly()
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            return _context.Products.Count();
        }
        public Product getProuductWithUset(int id)
        {
          return  _context.Products.Include(p => p.User).FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetAllWithUser()
        {
            return _context.Products
                .Where(p => p.Status != "Accepted" && p.Status != "Rejected")
                .Include(p => p.User)
                .Include(p => p.Category)

                .ToList();
        }

    }
}