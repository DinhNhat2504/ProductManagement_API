using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddNewProduct([FromForm] ProductDTO dto)
        {
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);
                var filePath = Path.Combine("wwwroot/images/products", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                dto.ImageURL = "/images/products/" + fileName;
            }

            var product = await _productService.AddProductAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        // Sửa sản phẩm
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDTO dto)
        {
            if (id != dto.ProductId)
                return BadRequest("Product ID mismatch");

            // Lấy thông tin sản phẩm cũ
            var oldProduct = await _productService.GetProductByIdAsync(id);

            // Nếu có file ảnh mới
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                // Xóa ảnh cũ nếu tồn tại
                if (!string.IsNullOrEmpty(oldProduct?.ImageURL))
                {
                    var oldImagePath = Path.Combine("wwwroot", oldProduct.ImageURL.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

                // Lưu ảnh mới
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);
                var filePath = Path.Combine("wwwroot/images/products", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                dto.ImageURL = "/images/products/" + fileName;
            }
            else
            {
                // Nếu không upload ảnh mới, giữ nguyên ảnh cũ
                dto.ImageURL = oldProduct?.ImageURL;
            }

            var result = await _productService.UpdateProductAsync(id, dto);
            if (!result)
                return NotFound();
            return Ok();
        }
        // Upload ảnh sản phẩm
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadProductImage([FromForm] ProductImageUploadDTO dto)
        {
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);
                var filePath = Path.Combine("wwwroot/images/products", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                // Cập nhật đường dẫn ảnh cho sản phẩm
                await _productService.UpdateProductImageAsync(dto.ProductId, "/images/products/" + fileName);
            }

            return Ok(new { message = "Upload thành công!" });
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
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedProducts(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string? searchTerm = null)
        {
            var products = await _productService.GetPagedProductsAsync(pageNumber, pageSize, searchTerm);
            return Ok(products);
        }
    }
}