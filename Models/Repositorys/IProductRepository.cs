﻿using Microsoft.EntityFrameworkCore;

namespace Mvc_Project.Models.Repositorys
{
    namespace Mvc_Project.Models.Repositorys
    {
        public interface IProductRepository : ICrudOperation<Product>
        {
            List<Product> GetProductsByCategory(int categoryId);
            List<Product> SearchProducts(string searchTerm);
            List<Category> GetAllCategories();
            List<Product> GetAllRandomly();
            public List<Product> GetAllWithUser();
            public List<Product> GetAllRejected();
            public Product getProuductWithUset(int id);
            public List<Product> SearchAdvancedProducts(string searchTerm);



          

            List<Product> GetAllRandomly(int pageNumber, int pageSize);
            int Count();

            public int GetProductCountByCategory(int categoryId);




        }
    }

}
