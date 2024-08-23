using Microsoft.AspNetCore.Identity;

namespace Mvc_Project.Models
{
    public class User : IdentityUser<int>
    {
       


       /* public int Id { get; set; }*/
     
        public bool EmailVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<UserRole>? UserRoles { get; set; } 
    
        public List<ShoppingCart>? ShoppingCarts { get; set; } 
        public List<Order>? Orders { get; set; } 
        public List<ProductReview>? ProductReviews { get; set; }
        public List<ProductQuestion>? ProductQuestions { get; set; } 
        public List<Wishlist>? Wishlists { get; set; }
        public List<PaymentMethod>? PaymentMethods { get; set; } 
        public List<UserPreference>? UserPreferences { get; set; }



      
           
        
    }

}
