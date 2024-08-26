namespace Mvc_Project.Models.ViewModels
{
    public class DashboardViewModel
    {

        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public int TotalOrders { get; set; }
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
    }
}
