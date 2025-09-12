using ProductManagement.DTOs;

namespace ProductManagement.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int productId);
        Task<ProductDTO> AddProductAsync(ProductDTO productDto);
        Task<bool> UpdateProductAsync(int productId, ProductDTO productDto);
        Task<bool> DeleteProductAsync(int productId);
        Task<IEnumerable<ProductDTO>> SearchProductsAsync(string searchTerm);
        Task<PagedResult<ProductDTO>> FilterProductsAsync(int? categoryId, decimal? minPrice, decimal? maxPrice, string? sortBy, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<ProductDTO>> GetFeaturedProductsAsync();
        Task<IEnumerable<ProductReviewDTO>> GetProductReviewsAsync(int productId);
        Task<IEnumerable<ProductDTO>> GetRelatedProductsAsync(int productId);
        Task<PagedResult<ProductDTO>> GetPagedProductsAsync(int pageNumber, int pageSize, string? searchTerm);
        Task<bool> UpdateProductImageAsync(int productId, string imageUrl);
    }
}