using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
  
        public class ProductReview
        {
            public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
            public Product Product { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
            public User User { get; set; }
            public string Comment { get; set; }
            public int Rating { get; set; } // Rating between 1 and 5
            public bool VerifiedReview { get; set; }
            public DateTime CreatedAt { get; set; }
        }
 }