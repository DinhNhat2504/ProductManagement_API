using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class WishlistItem
{
    [Key]
    public int WishlistItemId { get; set; }

    [Required]
    [ForeignKey("Wishlist")]
    public int WishlistId { get; set; }
    public Wishlist? Wishlist { get; set; }

    [Required]
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}