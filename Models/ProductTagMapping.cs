using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
    public class ProductTagMapping
    {
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("ProductTag")]
        public int TagId { get; set; }
        public ProductTag Tag { get; set; }
    }

}
