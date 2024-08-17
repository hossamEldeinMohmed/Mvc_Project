using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
    public class OrderStatusHistory
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string Status { get; set; } // 'pending', 'processing', 'shipped', etc.
        public DateTime StatusChangedAt { get; set; }
    }

}
