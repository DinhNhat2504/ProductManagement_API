using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models;

public class BlogCategory
{
    [Key]
    public int BlogCategoryId { get; set; }

    [Required]
    [StringLength(100)]
    public string? Name { get; set; } 

    [StringLength(200)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}