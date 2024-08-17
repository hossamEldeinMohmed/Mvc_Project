using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
    public class SellerProduct
    {
        public int Id { get; set; }
        [ForeignKey("Merchant")]
        public int MerchantId { get; set; }
        public Merchant Merchant { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
