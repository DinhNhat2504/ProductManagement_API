using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface IOrderStatusService
    {
        Task<IEnumerable<OrderStatus>> GetAllStatusAsync();
        Task<OrderStatus?> GetStatusByIdAsync(int statusId);
        
    }
}
