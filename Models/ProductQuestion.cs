using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
    public class ProductQuestion
    {
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsAnswered { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? AnsweredAt { get; set; }
    }

}
