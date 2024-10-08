﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Size { get; set; }
        public string? Address { get; set; }
        public string Description { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Status { get; set; } = "Pending";

        public List<ProductAttribute> ProductAttributes { get; set; } 
        public List<SellerProduct> SellerProducts { get; set; } 
        public List<ProductReview> ProductReviews { get; set; } 
        public List<ProductQuestion> ProductQuestions { get; set; }
        public List<ProductTagMapping> ProductTagMappings { get; set; } 
        public List<WishlistItem> WishlistItems { get; set; } 
        public List<CartItem> CartItems { get; set; }
      

        public List<string>? ProductImges { get; set; }
    }

}
