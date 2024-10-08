﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Mvc_Project.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Size { get; set; }
        public string? Address { get; set; }
        public string Description { get; set; }
        public string? CategoryName { get; set; }
        public int Price { get; set; }
        public List<string>? ProductImages { get; set; }

        // Add the CategoryList property
        public List<SelectListItem>? CategoryList { get; set; }

        // Add CategoryId for binding the selected category
        public int? CategoryId { get; set; }
    }
}
