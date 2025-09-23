using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class Brand
{
    [Key]
    public int BrandId { get; set; }

    [Required]
    [StringLength(150)]
    public string? Name { get; set; }

    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [StringLength(500)]
    public string? ImageURL { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
