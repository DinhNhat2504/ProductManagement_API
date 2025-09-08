using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusService _orderStatusService;
        public OrderStatusController(IOrderStatusService orderStatusService)
        {
            _orderStatusService = orderStatusService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStatus()
        {
            var statuses = await _orderStatusService.GetAllStatusAsync();
            return Ok(statuses);
        }
        [HttpGet("{statusId}")]
        public async Task<IActionResult> GetStatusById(int statusId)
        {
            var status = await _orderStatusService.GetStatusByIdAsync(statusId);
            if (status == null)
                return NotFound();
            return Ok(status);
        }
    }
}
