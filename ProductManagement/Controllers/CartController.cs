using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProductManagement.DTOs;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Lấy giỏ hàng của khách hàng
        
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }

        // Lấy danh sách sản phẩm trong giỏ hàng
        [Authorize]
        [HttpGet("{userId}/items")]
        public async Task<IActionResult> GetCartItems(int userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            if (cart == null) return NotFound();
            var items = await _cartService.GetCartItemsAsync(cart.CartId);
            return Ok(items);
        }

        // Thêm hoặc cập nhật sản phẩm vào giỏ hàng
        [Authorize]
        [HttpPost("{userId}/items")]
        public async Task<IActionResult> AddOrUpdateCartItem(int userId, [FromBody] CartItemDTO dto)
        {
            var result = await _cartService.AddOrUpdateCartItemAsync(userId, dto);
            if (!result) return BadRequest();
            return Ok();
        }
        [Authorize]
        [HttpPut("{userId}/items")]
        public async Task<IActionResult> UpdateQuantityItem(int userId, [FromBody] CartItemDTO dto)
        {
            var result = await _cartService.UpdateQuantityCart(userId, dto);
            if (!result) return BadRequest();
            return Ok();
        }

        // Xóa sản phẩm khỏi giỏ hàng
        [Authorize]
        [HttpDelete("{userId}/items/{productId}")]
        public async Task<IActionResult> RemoveCartItem(int userId, int productId)
        {
            var result = await _cartService.RemoveCartItemAsync(userId, productId);
            if (!result) return NotFound();
            return NoContent();
        }

        // Xóa toàn bộ giỏ hàng
        [Authorize]
        [HttpDelete("{userId}/clear")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var result = await _cartService.ClearCartAsync(userId);
            if (!result) return NotFound();
            return NoContent();
        }
        // Lấy giỏ hàng theo ID
        [Authorize]
        [HttpGet("cart/{cartId}")]
        public async Task<IActionResult> GetCartById(int cartId)
        {
            var cart = await _cartService.GetCartByIdAsync(cartId);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        
    }
}
