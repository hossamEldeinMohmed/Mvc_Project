using System.Collections.Generic;
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
                .Include(p => p.Category)
                .Include(p => p.ProductAttributes)
                .Include(p => p.ProductReviews)
                .Include(P =>P.ProductImges)
                .ToList();
        }
        public List<Product> GetAllRandomly()
        {
            return _context.Products
                .Include(p => p.Category)                
                .Include(p => p.ProductAttributes)
                .Include(p => p.ProductReviews)
                .OrderBy(p => Guid.NewGuid())  // Randomly sort the products
                .ToList();
        }
        public Product GetByID(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductAttributes)
                .Include(p => p.ProductReviews)
                .Include(P=>P.ProductImges)
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


    }
}