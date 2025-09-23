using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTOs;
using ProductManagement.Services;
using System.Security.Claims;

namespace ProductManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IEmailService _emailService;
        public OrderController(IOrderService orderService, IEmailService emailService)
        {
            _orderService = orderService;
            _emailService = emailService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy đơn hàng: {ex.Message}");
            }

        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            try
            {
                if (orderId == Guid.Empty)
                    return BadRequest("OrderId không hợp lệ!");
                var order = await _orderService.GetOrderByIdAsync(orderId);
                if (order == null)
                    return NotFound();
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy đơn hàng: {ex.Message}");
            }

        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByUserIdAsync(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy đơn hàng: {ex.Message}");
            }

        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO orderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdOrder = await _orderService.CreateOrderAsync(orderDto);

                var confirmationLink = Url.Action(
                    nameof(ConfirmOrder),
                    "Order",
                    new { orderId = createdOrder.OrderId },
                    Request.Scheme
                );

                await _emailService.SendOrderConfirmationEmail(createdOrder, confirmationLink);

                return CreatedAtAction(nameof(GetOrderById), new { orderId = createdOrder.OrderId }, createdOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi tạo đơn hàng: {ex.Message}");
            }

        }
        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmOrder(Guid orderId)
        {
            try
            {
                if (orderId == Guid.Empty)
                    return BadRequest("OrderId không hợp lệ!");
                var updateDto = new OrderUpdateDTO { OrderStatusId = 3 }; // Đã xác nhận
                var updatedOrder = await _orderService.UpdateOrderStatusAsync(orderId, updateDto);
                if (updatedOrder == null)
                    return NotFound("Không tìm thấy đơn hàng!");

                return Redirect($"http://localhost:5173/order/confirm?orderId={orderId}&success=true");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xác nhận đơn hàng: {ex.Message}");
            }


        }
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] OrderUpdateDTO updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var updatedOrder = await _orderService.UpdateOrderStatusAsync(orderId, updateDto);
                if (updatedOrder == null)
                    return NotFound();
                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật trạng thái đơn hàng: {ex.Message}");

            }
        }
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            try
            {
                if (orderId == Guid.Empty)
                    return BadRequest("OrderId không hợp lệ!");
                var result = await _orderService.DeleteOrderAsync(orderId);
                if (!result)
                    return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa đơn hàng: {ex.Message}");
            }
            //[HttpPost("fromCart")]
            //public async Task<IActionResult> CreateOrderFromCart([FromBody] OrderCreateDTO orderDto , int UserId)
            //{
            //    if (!ModelState.IsValid)
            //        return BadRequest(ModelState);

            //    var createdOrder = await _orderService.CreateOrderFromCartAsync(UserId, orderDto);
            //    return CreatedAtAction(
            //        nameof(GetOrderById),
            //        new { orderId = createdOrder.OrderId },
            //        createdOrder
            //    );
            //}
        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetAllOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20, [FromQuery] string? searchTerm = null, int statusId = 0, int paymentId = 0)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                    return BadRequest("PageNumber và PageSize phải lớn hơn 0.");
                var orders = await _orderService.GetPagedOrderAsync(pageNumber, pageSize, searchTerm, statusId, paymentId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy đơn hàng: {ex.Message}");
            }

        }
    }
}
