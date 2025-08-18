using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) {
            _context = context;
        }
        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
             await _context.SaveChangesAsync();

        }

        

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

        }

        public Task DeleteProductPriceAsync(int priceId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(g => g.ProductReviews)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products.Include(c => c.Category).Where(p => p.CategoryId == categoryId).ToListAsync();

        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Name!.Contains(searchTerm) || p.Description!.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
             await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Product>> FilterProductsAsync(int? categoryId, decimal? minPrice, decimal? maxPrice, string? sortBy)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);
            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "price_asc":
                        query = query.OrderBy(p => p.Price);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(p => p.Price);
                        break;
                    case "newest":
                        query = query.OrderByDescending(p => p.CreatedAt);
                        break;
                    default:
                        query = query.OrderBy(p => p.Name!);
                        break;
                }
            }

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetFeaturedProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsFeatured)
                .ToListAsync();
        }
        public async Task<IEnumerable<ProductReview>> GetProductReviewsAsync(int productId)
        {
            return await _context.Set<ProductReview>()
                .Where(r => r.ProductId == productId)
                .Include(r => r.User)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetRelatedProductsAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return new List<Product>();

            return await _context.Products
                .Where(p => p.CategoryId == product.CategoryId && p.ProductId != productId)
                .Take(5)
                .ToListAsync();
        }
    }
}
