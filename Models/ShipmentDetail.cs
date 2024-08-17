using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
    public class ShipmentDetail
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }
        public DateTime ShippedAt { get; set; }
        public DateTime EstimatedDelivery { get; set; }
        public DateTime DeliveredAt { get; set; }
    }
}