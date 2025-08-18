using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models;

public class OrderStatus
{
    [Key]
    public int OrderStatusId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}