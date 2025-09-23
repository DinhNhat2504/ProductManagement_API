using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.DTOs
{
    public class BrandDTO
    {
        public int BrandId { get; set; }

        [Required]
        [StringLength(150)]
        public string? Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string? ImageURL { get; set; }
    }
}
