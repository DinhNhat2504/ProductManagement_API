using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [ForeignKey("User")]
    public int? UserId { get; set; }
    [StringLength(100)]
    public string? CustomerName { get; set; }

    [StringLength(100)]
    [EmailAddress]
    public string? CustomerEmail { get; set; }

    [StringLength(20)]
    public string? CustomerPhone { get; set; }

    [ForeignKey("Voucher")]
    public int? VoucherId { get; set; }

    public DateTime OrderDate { get; set; } 

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    [Required]
    [ForeignKey("OrderStatus")]
    public int OrderStatusId { get; set; }  

    [Required]
    [ForeignKey("Payment")]
    public int PaymentId { get; set; }

    [StringLength(200)]
    public string? ShippingAddress { get; set; }

    [StringLength(100)]
    public string? ShippingProvince { get; set; }

    [StringLength(100)]
    public string? ShippingDistrict { get; set; }

    [StringLength(100)]
    public string? ShippingWard { get; set; }

    public User? User { get; set; }
    public Voucher? Voucher { get; set; }
    public OrderStatus? OrderStatus { get; set; }
    public Payment? Payment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}