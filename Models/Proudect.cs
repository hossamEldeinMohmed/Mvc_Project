namespace Mvc_Project.Models
{
    public class Proudect
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Image { get; set; }
        public string Description { get; set; } 
        public string Category { get; set; } 
        public double Price { get; set; } 
        public bool IsSold { get; set; } // Indicates if the product is sold or not
        public DateTime DateAdded { get; set; } // Date when the product was added
    }
}
