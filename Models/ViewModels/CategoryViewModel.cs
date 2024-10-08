﻿namespace Mvc_Project.Models.ViewModels
{
    public class CategoryViewModel
    {
        public List<Category> Categories { get; set; }
        public Category NewCategory { get; set; }

        public Dictionary<int, int> ProductCounts { get; set; }
    }
}
