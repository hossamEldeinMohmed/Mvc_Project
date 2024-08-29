using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc_Project.Models
{
    public class Notification
    {
       
            public int Id { get; set; }
          
            public string Message { get; set; }
            public bool IsRead { get; set; } = false;
            public DateTime CreatedAt { get; set; } = DateTime.Now;

            [ForeignKey("User")]
            public int UserId { get; set; }
            public User User { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
