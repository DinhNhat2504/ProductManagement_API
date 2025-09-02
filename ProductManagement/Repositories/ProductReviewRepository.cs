using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly AppDbContext _context;
        public ProductReviewRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddProductReviewAsync(ProductReview review)
        {
            _context.ProductReviews.Add(review);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductReviewAsync(int reviewId)
        {
            var review = await _context.ProductReviews.FindAsync(reviewId);
            if (review != null)
            {
                _context.ProductReviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<ProductReview?> GetReviewByIdAsync(int reviewId)
        {
            return await _context.ProductReviews.FindAsync(reviewId);
        }
        public async Task<IEnumerable<ProductReview>> GetReviewsByProductIdAsync(int productId)
        {
            return await _context.ProductReviews
                .Where(r => r.ProductId == productId)
                .ToListAsync();
        }
        public async Task UpdateProductReviewAsync(ProductReview review)
        {
            _context.ProductReviews.Update(review);
            await _context.SaveChangesAsync();
        }
    }
}
