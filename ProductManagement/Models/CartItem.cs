using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class CartItem
{
    [Key]
    public int CartItemId { get; set; }

    [Required]
    [ForeignKey("Cart")]
    public int CartId { get; set; }

    [Required]
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    public Cart? Cart { get; set; }
    public Product? Product { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}