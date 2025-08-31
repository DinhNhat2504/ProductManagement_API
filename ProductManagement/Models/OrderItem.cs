using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    [Required]
    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [Required]
    [ForeignKey("Product")]
    public int ProductId { get; set; }  

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public Order? Order { get; set; }
    public Product? Product { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}