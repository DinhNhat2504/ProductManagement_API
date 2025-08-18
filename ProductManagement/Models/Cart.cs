using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class Cart
{
    [Key]
    public int CartId { get; set; }

    [ForeignKey("User")]
    public int? UserId { get; set; }

    public User? User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}