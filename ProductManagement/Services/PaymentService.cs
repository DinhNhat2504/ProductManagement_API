using ProductManagement.Models;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task CreatePaymentAsync(Payment payment)
        {
            await _paymentRepository.CreatePaymentAsync(payment);
        }

        public async Task<bool> DeletePaymentAsync(int paymentId)
        {
            return await _paymentRepository.DeletePaymentAsync(paymentId);
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAllPaymentsAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int paymentId)
        {
            return await _paymentRepository.GetPaymentByIdAsync(paymentId);
        }

        public async Task<bool> UpdatePaymentAsync(Payment payment)
        {
            var existingPayment = await _paymentRepository.GetPaymentByIdAsync(payment.PaymentId);
            if (existingPayment == null)
                return false;
           
            await _paymentRepository.UpdatePaymentAsync(payment);
            return true;
        }
    }
}
