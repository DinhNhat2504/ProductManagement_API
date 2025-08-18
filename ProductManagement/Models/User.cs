using ProductManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace ProductManagement.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [ForeignKey("Roles")]
    public int RoleId { get; set; }

    public Role? Role { get; set; }

    [Required]
    [StringLength(50)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string? LastName { get; set; } 

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string? Email { get; set; } 

    [Required]
    [StringLength(256)]
    public string? HashedPassword { get; set; } 

    [StringLength(200)]
    public string? Address { get; set; } 

    [StringLength(20)]
    public string? PhoneNumber { get; set; } 

    public string? AvatarUrl { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Cart? Cart { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    public ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
    public ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public ICollection<ChatRoom> ChatRoomsAsUser { get; set; } = new List<ChatRoom>(); 
    public ICollection<ChatRoom> ChatRoomsAsAdmin { get; set; } = new List<ChatRoom>(); 
    public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}