using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Mvc_Project.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? CategoryName { get; set; }
        public decimal Price { get; set; }
        public List<string>? ProductImages { get; set; }

        // Add the CategoryList property
        public List<SelectListItem>? CategoryList { get; set; }

        // Add CategoryId for binding the selected category
        public int? CategoryId { get; set; }
    }
}
