using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{userId}/items")]
        public async Task<IActionResult> GetCartItems(int userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            if (cart == null) return NotFound();
            var items = await _cartService.GetCartItemsAsync(cart.CartId);
            return Ok(items);
        }

        // Thêm hoặc cập nhật sản phẩm vào giỏ hàng
        [HttpPost("{userId}/items")]
        public async Task<IActionResult> AddOrUpdateCartItem(int userId, [FromBody] CartItemDTO dto)
        {
            var result = await _cartService.AddOrUpdateCartItemAsync(userId, dto);
            if (!result) return BadRequest();
            return Ok();
        }

        // Xóa sản phẩm khỏi giỏ hàng
        [HttpDelete("{userId}/items/{productId}")]
        public async Task<IActionResult> RemoveCartItem(int userId, int productId)
        {
            var result = await _cartService.RemoveCartItemAsync(userId, productId);
            if (!result) return NotFound();
            return NoContent();
        }

        // Xóa toàn bộ giỏ hàng
        [HttpDelete("{userId}/clear")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var result = await _cartService.ClearCartAsync(userId);
            if (!result) return NotFound();
            return NoContent();
        }
        // Lấy giỏ hàng theo ID
        [HttpGet("cart/{cartId}")]
        public async Task<IActionResult> GetCartById(int cartId)
        {
            var cart = await _cartService.GetCartByIdAsync(cartId);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        // Thêm hoặc cập nhật sản phẩm vào giỏ hàng theo ID giỏ hàng
        [HttpPost("cart/{cartId}/items")]
        public async Task<IActionResult> AddOrUpdateCartItemByCartId(int cartId, [FromBody] CartItemDTO dto)
        {
            var result = await _cartService.AddOrUpdateCartItemByCartIdAsync(cartId, dto);
            if (!result) return BadRequest();
            return Ok();
        }
        // Xóa sản phẩm khỏi giỏ hàng theo ID giỏ hàng
        [HttpDelete("cart/{cartId}/items/{productId}")]
        public async Task<IActionResult> RemoveCartItemByCartId(int cartId, int productId)
        {
            var result = await _cartService.RemoveCartItemByCartIdAsync(cartId, productId);
            if (!result) return NotFound();
            return NoContent();
        }
        // Xóa toàn bộ giỏ hàng theo ID giỏ hàng
        [HttpDelete("cart/{cartId}/clear")]
        public async Task<IActionResult> ClearCartByCartId(int cartId)
        {
            var result = await _cartService.ClearCartByCartIdAsync(cartId);
            if (!result) return NotFound();
            return NoContent();
        }
        // Tạo giỏ hàng cho khách
        [HttpPost("guest")]
        public async Task<IActionResult> CreateGuestCart()
        {
            var cart = await _cartService.CreateGuestCartAsync();
            if (cart == null) return BadRequest("Unable to create guest cart.");
            return CreatedAtAction(nameof(GetCartById), new { cartId = cart.CartId }, cart);
        }
    }
}
