using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public interface IOrderStatusRepository
    {
        Task<IEnumerable<OrderStatus>> GetAllStatusAsync();
        Task<OrderStatus?> GetStatusByIdAsync(int statusId);
    }
}
