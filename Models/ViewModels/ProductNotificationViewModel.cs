namespace Mvc_Project.Models.ViewModels
{
    public class ProductNotificationViewModel
    {
        public int ProductId { get; set; }
        public int AdminId { get; set; }
        public string ProductName { get; set; }
        public string RejectionReason { get; set; }
        public DateTime RejectionDate { get; set; }
    }
}
