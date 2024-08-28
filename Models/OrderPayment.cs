using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
    public class OrderPayment
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [ForeignKey("PaymentMethod")]
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
