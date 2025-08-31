using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface IPaymentService
    {
        Task<List<Payment>> GetAllPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(int paymentId);
        Task CreatePaymentAsync(Payment payment);   
        Task<bool> UpdatePaymentAsync( Payment payment);
        Task<bool> DeletePaymentAsync(int paymentId);
    }
}
