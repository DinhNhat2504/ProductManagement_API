using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class Wishlist
{
    [Key]
    public int WishlistId { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User? User { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
}