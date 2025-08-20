using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpGet("methods")]
        public IActionResult GetPaymentMethods()
        {
            var methods = new List<string> { "COD", "BankTransfer", "VNPay", "Momo" };
            return Ok(methods);
        }
    }
}
