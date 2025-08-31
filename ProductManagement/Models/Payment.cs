using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class Payment
{
    [Key]
    public int PaymentId { get; set; }

    [Required]
    [StringLength(50)]
    public string? PaymentMethod { get; set; } = "COD";

    [StringLength(200)]
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    //public ICollection<Order> Orders { get; set; } = new List<Order>();
}