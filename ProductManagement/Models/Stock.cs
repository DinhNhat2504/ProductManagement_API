using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class Stock
{
    [Key]
    public int StockId { get; set; }

    [Required]
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    public DateTime LastUpdated { get; set; }
}