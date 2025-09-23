using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUserStats([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try {
                
                var data = await _statisticsService.GetUserStatisticsAsync(fromDate, toDate);
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }
        [HttpGet("products")]
        public async Task<IActionResult> GetProductStats([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try {
                
                var data = await _statisticsService.GetProductStatisticsAsync(fromDate, toDate);
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }
        [HttpGet("import")]
        public async Task<IActionResult> GetImportStats([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try {
                
                var data = await _statisticsService.GetImportStockStatisticsAsync(fromDate, toDate);
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }
        [HttpGet("export")]
        public async Task<IActionResult> GetExportStats([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try {
                
                var data = await _statisticsService.GetExportStockStatisticsAsync(fromDate, toDate);
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }
        [HttpGet("orders")]
        public async Task<IActionResult> GetOrderStats([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try {
                
                var data = await _statisticsService.GetOrderStatisticsAsync(fromDate, toDate);
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }
        [HttpGet("revenue")]
        public async Task<IActionResult> GetRevenueStats([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try {
                
                var data = await _statisticsService.GetRevenueStatisticsAsync(fromDate, toDate);
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }
    }
}
