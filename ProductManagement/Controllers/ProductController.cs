using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTOs;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // Lấy tất cả sản phẩm
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // Lấy sản phẩm theo id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // Thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductDTO dto)
        {
            var product = await _productService.AddProductAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        // Sửa sản phẩm
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO dto)
        {
            if (id != dto.ProductId)
                return BadRequest("Product ID mismatch");

            var result = await _productService.UpdateProductAsync(id, dto);
            if (!result)
                return NotFound();
            return Ok();
        }

        // Xóa sản phẩm
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        // Lấy sản phẩm theo category
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductByCategory(int categoryId)
        {
            var products = await _productService.FilterProductsAsync(categoryId, null, null, null);
            return Ok(products);
        }

        // Tìm kiếm sản phẩm
        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string query)
        {
            var products = await _productService.SearchProductsAsync(query);
            return Ok(products);
        }

        // Lọc sản phẩm nâng cao
        [HttpGet("filter")]
        public async Task<IActionResult> FilterProducts([FromQuery] int? categoryId, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] string? sortBy)
        {
            var products = await _productService.FilterProductsAsync(categoryId, minPrice, maxPrice, sortBy);
            return Ok(products);
        }

        // Lấy sản phẩm nổi bật
        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedProducts()
        {
            var products = await _productService.GetFeaturedProductsAsync();
            return Ok(products);
        }

        // Lấy review của sản phẩm
        [HttpGet("{id}/reviews")]
        public async Task<IActionResult> GetProductReviews(int id)
        {
            var reviews = await _productService.GetProductReviewsAsync(id);
            return Ok(reviews);
        }

        // Lấy sản phẩm liên quan
        [HttpGet("{id}/related")]
        public async Task<IActionResult> GetRelatedProducts(int id)
        {
            var products = await _productService.GetRelatedProductsAsync(id);
            return Ok(products);
        }
    }
}