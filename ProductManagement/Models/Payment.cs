using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class Payment
{
    [Key]
    public int PaymentId { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(50)]
    public string? PaymentMethod { get; set; } 

    public DateTime PaymentDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}