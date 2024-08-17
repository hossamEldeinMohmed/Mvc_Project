using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Status { get; set; } // 'open', 'closed'
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }

}
