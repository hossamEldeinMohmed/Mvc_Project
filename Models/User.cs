using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Mvc_Project.Models
{
    public class User : IdentityUser<int>
    {



        /* public int Id { get; set; }*/
        public string ProfileImageUrl { get; set; } // Add this property for the image

        public bool EmailVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public List<UserRole>? UserRoles { get; set; }
        public List<Product>? Products { get; set; }
        public List<ShoppingCart>? ShoppingCarts { get; set; } 
        public List<Order>? Orders { get; set; } 
        public List<ProductReview>? ProductReviews { get; set; }
        public List<ProductQuestion>? ProductQuestions { get; set; } 
        public List<Wishlist>? Wishlists { get; set; }
        public List<PaymentMethod>? PaymentMethods { get; set; } 
        public List<UserPreference>? UserPreferences { get; set; }



      
           
        
    }

}
