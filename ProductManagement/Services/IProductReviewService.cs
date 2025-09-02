using ProductManagement.DTOs;
using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface IProductReviewService
    {
        Task AddProductReviewAsync(ProductReviewDTO dto);
        Task DeleteProductReviewAsync(int reviewId);
        Task<ProductReviewDTO?> GetReviewByIdAsync(int reviewId);
        Task UpdateProductReviewAsync(int reviewId , ProductReviewDTO dto);
    }
}
