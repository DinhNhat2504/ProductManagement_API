using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public interface IProductReviewRepository
    {
        Task AddProductReviewAsync(ProductReview review);
        Task<IEnumerable<ProductReview>> GetReviewsByProductIdAsync(int productId);
        Task DeleteProductReviewAsync(int reviewId);
        Task<ProductReview?> GetReviewByIdAsync(int reviewId);
        Task UpdateProductReviewAsync(ProductReview review);
    }
}
