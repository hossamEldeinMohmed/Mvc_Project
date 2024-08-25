﻿namespace Mvc_Project.Models.Repositorys
{
    namespace Mvc_Project.Models.Repositorys
    {
        public interface IProductRepository : ICrudOperation<Product>
        {
            List<Product> GetProductsByCategory(int categoryId);
            List<Product> SearchProducts(string searchTerm);
            List<Category> GetAllCategories();
            List<Product> GetAllRandomly(int pageNumber, int pageSize);
            int Count();
       



        }
    }

}
