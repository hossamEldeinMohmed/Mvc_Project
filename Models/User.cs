namespace Mvc_Project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public Merchant Merchant { get; set; }
        public List<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
        public List<ProductQuestion> ProductQuestions { get; set; } = new List<ProductQuestion>();
        public List<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public List<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
        public List<UserPreference> UserPreferences { get; set; } = new List<UserPreference>();
    }

}
