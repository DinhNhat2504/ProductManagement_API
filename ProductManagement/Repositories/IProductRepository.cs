using ProductManagement.Models;
using ProductManagement.Services;

namespace ProductManagement.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task<PagedResult<Product>> FilterProductsAsync(int? categoryId, decimal? minPrice, decimal? maxPrice, string? sortBy, int pageNumber,
    int pageSize);
        Task<IEnumerable<Product>> GetFeaturedProductsAsync();
        Task<IEnumerable<ProductReview>> GetProductReviewsAsync(int productId);
        Task<IEnumerable<Product>> GetRelatedProductsAsync(int productId);
    }
}
