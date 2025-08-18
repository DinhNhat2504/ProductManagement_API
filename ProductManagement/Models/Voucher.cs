using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class Voucher
{
    [Key]
    public int VoucherId { get; set; } 

    [Required]
    [StringLength(50)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string DiscountType { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountValue { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public int? MaxUsage { get; set; }
    public int CurrentUsage { get; set; }

    [ForeignKey("User")]
    public int? UserId { get; set; }

    public User? User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}