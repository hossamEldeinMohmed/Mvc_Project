using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Mvc_Project.Models.Repositorys
{
    public class CategoryRepository : ICategory
    {


        private readonly Context _context;

       
        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.Include(c => c.Products).ToList();
        }

        public Category GetByID(int id)
        {
            return _context.Categories.Include(c => c.Products)
                                      .FirstOrDefault(category => category.Id == id);
        }

        public void Add(Category newCategory)
        {
            newCategory.CreatedAt = DateTime.Now;
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
        }

        public void Update(Category updatedCategory)
        {
            var existingCategory = _context.Categories.Include(c => c.Products)
                                                      .FirstOrDefault(c => c.Id == updatedCategory.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = updatedCategory.Name;
                existingCategory.Description = updatedCategory.Description;
                existingCategory.UpdatedAt = DateTime.Now;

               
                existingCategory.Products = updatedCategory.Products;

                _context.SaveChanges();
            }
        }
        public Category GetByName(string name)
        {

            return _context.Categories
            .Where(c => c.Name.ToLower() == name.ToLower())
            .FirstOrDefault();
        }

        public void Delete(int id)
        {
            var categoryToRemove = _context.Categories.Include(c => c.Products)
                                                      .FirstOrDefault(c => c.Id == id);
            if (categoryToRemove != null)
            {
                _context.Categories.Remove(categoryToRemove);
                _context.SaveChanges();
            }
        }


    }
}
