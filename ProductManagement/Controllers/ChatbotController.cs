using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTOs;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IGeminiService _geminiService;
        public ChatbotController(IProductService productService, IOrderService orderService, ICartService cartService, IGeminiService geminiService)
        {
            _productService = productService;
            _orderService = orderService;
            _cartService = cartService;
            _geminiService = geminiService;
        }
        [HttpPost("chatbot")]
        public async Task<IActionResult> Chatbot([FromBody] ChatbotRequestDTO request)
        {
            var intentInfo = await _geminiService.ExtractIntentAsync(request.Message);

            switch (intentInfo.Intent)
            {
                case "ListProducts":
                    var products = await _productService.GetAllProductsAsync();
                    var replyList = await _geminiService.GetResponseAsync(
                        $"Danh sách sản phẩm hiện có: {string.Join(", ", products.Select(p => p.Name))}. Trả lời khách bằng tiếng Việt.");
                    return Ok(new { reply = replyList });

                case "OrderInfo":
                    // Lấy thông tin đơn hàng của user (cần truyền thêm userId)
                    var ordersFound = await _orderService.GetOrdersByUserIdAsync(request.UserId);
                    var orders = ordersFound.FirstOrDefault();
                    var replyOrder = await _geminiService.GetOrderAnswerAsync(orders, request.Message);
                    return Ok(new { reply = replyOrder });
              

                case "CartInfo":
                // Lấy thông tin giỏ hàng của user (cần truyền thêm userId)
                // var cart = await _cartService.GetCartByUserAsync(request.UserId);
                // Xử lý trả lời tương tự

                case "ProductDetail":
                    if (!string.IsNullOrEmpty(intentInfo.ProductName))
                    {
                        var productsFound = await _productService.SearchProductsAsync(intentInfo.ProductName);
                        var product = productsFound.FirstOrDefault();
                        if (product == null)
                            return NotFound("Không tìm thấy sản phẩm.");
                        var reply = await _geminiService.GetProductAnswerAsync(product, request.Message);
                        return Ok(new { reply });
                    }
                    return BadRequest("Không xác định được sản phẩm.");

                default:
                    var defaultReply = await _geminiService.GetResponseAsync("Xin chào! Bạn cần hỗ trợ gì về sản phẩm, đơn hàng hoặc giỏ hàng?");
                    return Ok(new { reply = defaultReply });
            }
        }
    }
}
