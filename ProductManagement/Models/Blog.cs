using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class Blog
{
    [Key]
    public int BlogId { get; set; }

    [Required]
    [ForeignKey("BlogCategory")]
    public int BlogCategoryId { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [StringLength(200)]
    public string? Title { get; set; }

    [Required]
    public string? Content { get; set; } 

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Draft";

    public DateTime? PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public BlogCategory? BlogCategory { get; set; }
    public User? User { get; set; }
}