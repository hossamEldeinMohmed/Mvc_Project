using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Mvc_Project.Models
{
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }


        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
      
        public string Address { get; set; }

        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // 'pending', 'processing', 'shipped', etc.
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<OrderStatusHistory> OrderStatusHistories { get; set; }
        public List<OrderPayment> OrderPayments { get; set; }
        public List<OrderCoupon> OrderCoupons { get; set; } 
        public ShipmentDetail ShipmentDetail { get; set; }
    }

}
