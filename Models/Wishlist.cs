namespace Mvc_Project.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<WishlistItem> WishlistItems { get; set; } 
    }

}
