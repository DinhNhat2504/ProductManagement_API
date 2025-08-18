using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    [StringLength(200)]
    public string? Name { get; set; } 

    [StringLength(500)]
    public string? Summary { get; set; } 

    [StringLength(1000)]
    public string? Description { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [StringLength(500)]
    public string? ImageURL { get; set; }

    public bool IsFeatured { get; set; } = false;
    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
}