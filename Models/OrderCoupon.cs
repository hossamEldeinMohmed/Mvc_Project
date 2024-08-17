using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
    public class OrderCoupon
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [ForeignKey("Coupon")]
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; }

        public decimal DiscountAmount { get; set; }
        public string DiscountType { get; set; } // 'percentage' or 'fixed'
        public DateTime CreatedAt { get; set; }
    }

}
