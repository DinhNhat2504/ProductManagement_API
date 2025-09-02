using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTOs;
using ProductManagement.Repositories;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly IProductReviewService _productReviewService;
        public ProductReviewController(IProductReviewService productReviewService)
        {
            _productReviewService = productReviewService;
        }
        // Thêm đánh giá cho sản phẩm
        [HttpPost("{productId}/review")]
        public async Task<IActionResult> AddReview(int productId, [FromBody] ProductReviewDTO dto)
        {
            
            dto.ProductId = productId;
            await _productReviewService.AddProductReviewAsync(dto);
            return CreatedAtAction(nameof(GetReviewById), new { reviewId = dto.ReviewId }, dto);
        }
        //
        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetReviewById(int reviewId)
        {
            var review = await _productReviewService.GetReviewByIdAsync(reviewId);
            if (review == null)
                return NotFound();
            return Ok(review);
        }
        // Sửa đánh giá
        [HttpPut("review/{reviewId}")]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ProductReviewDTO dto)
        {
            try
            {
                await _productReviewService.UpdateProductReviewAsync(reviewId, dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        // Xóa đánh giá
        [HttpDelete("review/{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            await _productReviewService.DeleteProductReviewAsync(reviewId);
            return NoContent();
        }
    }
}
