using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    
}