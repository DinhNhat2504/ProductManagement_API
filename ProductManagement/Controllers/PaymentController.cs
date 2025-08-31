using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPaymentMethods()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentMethodById(int id)
        {
            var payments = await _paymentService.GetPaymentByIdAsync(id);
            if (payments == null)
                return NotFound();
            return Ok(payments);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePaymentMethod([FromBody] Payment payment)
        {
            await _paymentService.CreatePaymentAsync(payment);
            return CreatedAtAction(nameof(GetPaymentMethodById), new { id = payment.PaymentId }, payment);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentMethod(int id, [FromBody] Payment payment)
        {
            if (id != payment.PaymentId)
                    return BadRequest("Payment ID mismatch");
            try
            {
                var existingPayment = await _paymentService.GetPaymentByIdAsync(id);
                if (existingPayment == null)
                    return NotFound();

                // Cập nhật các trường cần thiết
                existingPayment.PaymentMethod = payment.PaymentMethod;
                existingPayment.Description = payment.Description;
                existingPayment.UpdatedAt = DateTime.Now;

                await _paymentService.UpdatePaymentAsync(existingPayment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            var success = await _paymentService.DeletePaymentAsync(id);
            if (!success)
                return NotFound();
            return Ok();
        }
    }
}
