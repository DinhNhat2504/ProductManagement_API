using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(int paymentId);
        Task CreatePaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(int paymentId);
    }
}
