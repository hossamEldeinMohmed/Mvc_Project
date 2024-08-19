namespace Mvc_Project.Models
{
    public class ProductTag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<ProductTagMapping> ProductTagMappings { get; set; } 
    }

}
