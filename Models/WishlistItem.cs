using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
    public class WishlistItem
    {
        public int Id { get; set; }
        [ForeignKey("Wishlist")]

        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

}
