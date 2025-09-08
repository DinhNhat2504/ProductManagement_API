using ProductManagement.Models;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        public OrderStatusService(IOrderStatusRepository orderStatusRepository)
        {
            _orderStatusRepository = orderStatusRepository;
        }
        public async Task<IEnumerable<OrderStatus>> GetAllStatusAsync()
        {
            return await _orderStatusRepository.GetAllStatusAsync();
        }
        public async Task<OrderStatus?> GetStatusByIdAsync(int statusId)
        {
            return await _orderStatusRepository.GetStatusByIdAsync(statusId);
        }
    }
}
