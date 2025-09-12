using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class StockTransaction
{
    [Key]
    public int TransactionId { get; set; }

    [Required]
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    [Required]
    public int QuantityChanged { get; set; } // + nhập, - xuất

    public DateTime TransactionDate { get; set; }
    public string? Note { get; set; }
}
