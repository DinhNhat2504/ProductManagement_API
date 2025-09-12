using ProductManagement.DTOs;
using ProductManagement.Models;
using ProductManagement.Repositories;
using AutoMapper;

namespace ProductManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return products.Select(p => _mapper.Map<ProductDTO>(p));
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            return product == null ? null : _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> AddProductAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            await _productRepository.AddProductAsync(product);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> UpdateProductAsync(int productId, ProductDTO productDto)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(productId);
            if (existingProduct == null) return false;

            _mapper.Map(productDto, existingProduct);
            existingProduct.UpdatedAt = DateTime.Now;
            await _productRepository.UpdateProductAsync(existingProduct);
            return true;
        }
        public async Task<bool> UpdateProductImageAsync(int productId, string imageUrl)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null) return false;

            product.ImageURL = imageUrl;
            await _productRepository.UpdateProductAsync(product);
            return true;
        }
        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null) return false;

            await _productRepository.DeleteProductAsync(productId);
            return true;
        }

        public async Task<IEnumerable<ProductDTO>> SearchProductsAsync(string searchTerm)
        {
            var products = await _productRepository.SearchProductsAsync(searchTerm);
            return products.Select(p => _mapper.Map<ProductDTO>(p));
        }

        public async Task<PagedResult<ProductDTO>> FilterProductsAsync(
     int? categoryId,
     decimal? minPrice,
     decimal? maxPrice,
     string? sortBy,
     int pageNumber = 1,
     int pageSize = 10)
        {
            var pagedProducts = await _productRepository.FilterProductsAsync(categoryId, minPrice, maxPrice, sortBy, pageNumber, pageSize);
            return new PagedResult<ProductDTO>
            {
                Items = pagedProducts.Items.Select(p => _mapper.Map<ProductDTO>(p)),
                TotalCount = pagedProducts.TotalCount,
                PageNumber = pagedProducts.PageNumber,
                PageSize = pagedProducts.PageSize
            };
        }

        public async Task<IEnumerable<ProductDTO>> GetFeaturedProductsAsync()
        {
            var products = await _productRepository.GetFeaturedProductsAsync();
            return products.Select(p => _mapper.Map<ProductDTO>(p));
        }

        public async Task<IEnumerable<ProductReviewDTO>> GetProductReviewsAsync(int productId)
        {
            var reviews = await _productRepository.GetProductReviewsAsync(productId);
            return reviews.Select(r => _mapper.Map<ProductReviewDTO>(r));
        }

        public async Task<IEnumerable<ProductDTO>> GetRelatedProductsAsync(int productId)
        {
            var products = await _productRepository.GetRelatedProductsAsync(productId);
            return products.Select(p => _mapper.Map<ProductDTO>(p));
        }
        
        public async Task<PagedResult<ProductDTO>> GetPagedProductsAsync(int pageNumber, int pageSize, string? searchTerm)
        {
            var allProducts = string.IsNullOrWhiteSpace(searchTerm)
                ? await _productRepository.GetAllProductsAsync()
                : await _productRepository.SearchProductsAsync(searchTerm);
            var totalItems = allProducts.Count();
            var items = allProducts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => _mapper.Map<ProductDTO>(p))
                .ToList();
            return new PagedResult<ProductDTO>
            {
                Items = items,
                TotalCount = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

    }
}