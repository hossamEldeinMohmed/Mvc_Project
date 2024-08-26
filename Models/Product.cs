using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal Price { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; } 
        public List<SellerProduct> SellerProducts { get; set; } 
        public List<ProductReview> ProductReviews { get; set; } 
        public List<ProductQuestion> ProductQuestions { get; set; }
        public List<ProductTagMapping> ProductTagMappings { get; set; } 
        public List<WishlistItem> WishlistItems { get; set; } 
        public List<CartItem> CartItems { get; set; } 

        public List<ProductImages> ProductImges { get; set; }= new List<ProductImages>();
    }

}
