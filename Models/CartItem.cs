using Mvc_Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    [ForeignKey("ShoppingCart")]
    public int ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }

    public int Quantity { get; set; }
}
