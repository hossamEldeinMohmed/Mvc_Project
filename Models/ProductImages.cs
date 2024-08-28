namespace Mvc_Project.Models
{
    public class ProductImages
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public Product? Product { get; set; }
        public int ProductId { get; set; }
    }
}
