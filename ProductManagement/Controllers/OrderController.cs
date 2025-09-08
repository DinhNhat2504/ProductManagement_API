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
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        { 
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
                return NotFound();
            return Ok(order);
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdOrder = await _orderService.CreateOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { orderId = createdOrder.OrderId }, createdOrder);
        }
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] OrderUpdateDTO updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var updatedOrder = await _orderService.UpdateOrderStatusAsync(orderId, updateDto);
            if (updatedOrder == null)
                return NotFound();
            return Ok(updatedOrder);
        }
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var result = await _orderService.DeleteOrderAsync(orderId);
            if (!result)
                return NotFound();
            return NoContent();
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
        [HttpGet("paged")]
        public async Task<IActionResult> GetAllOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20, [FromQuery] string? searchTerm = null, int statusId = 0, int paymentId = 0)
        {
            var orders = await _orderService.GetPagedOrderAsync(pageNumber, pageSize, searchTerm , statusId, paymentId);
            return Ok(orders);
        }
    }
}
