namespace ProductManagement.DTOs
{
    public class ProductImageUploadDTO
    {
        public int ProductId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
