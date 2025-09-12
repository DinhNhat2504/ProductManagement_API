using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTOs;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportStock([FromBody] StockTransactionDTO dto)
        {
            await _stockService.ImportStockAsync(dto);
            return Ok("Nhập kho thành công");
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportStock([FromBody] StockTransactionDTO dto)
        {
            var result = await _stockService.ExportStockAsync(dto);
            if (!result)
                return BadRequest("Số lượng tồn kho không đủ");
            return Ok("Xuất kho thành công");
        }

        [HttpGet("{productId}/transactions")]
        public async Task<IActionResult> GetTransactions(int productId)
        {
            var transactions = await _stockService.GetTransactionsAsync(productId);
            return Ok(transactions);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetStock(int productId)
        {
            var stock = await _stockService.GetStockByProductIdAsync(productId);
            if (stock == null)
                return NotFound();
            return Ok(stock);
        }
    }
}
