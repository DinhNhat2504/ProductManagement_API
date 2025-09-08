using System.ComponentModel.DataAnnotations;

namespace ProductManagement.DTOs;

public class ProductDTO
{
    public int ProductId { get; set; }
    [Required]
    [StringLength(200)]
    public string? Name { get; set; }

    [StringLength(500)]
    public string? Summary { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }
    public bool IsFeatured { get; set; } = false;

    [StringLength(500)]
    public string? ImageURL { get; set; }

    public IFormFile? ImageFile { get; set; } 
    [Required]
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }


}