using ProductManagement.DTOs;

namespace ProductManagement.Services
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(int orderId);
        Task<List<OrderDTO>> GetOrdersByUserIdAsync(int userId);
        Task<OrderDTO> CreateOrderAsync(OrderCreateDTO orderDto);
        Task<OrderDTO?> UpdateOrderStatusAsync(int orderId, OrderUpdateDTO updateDto);
        Task<bool> DeleteOrderAsync(int orderId);
        Task<OrderDTO> CreateOrderFromCartAsync(int userId, OrderCreateDTO orderDto);
        Task<PagedResult<OrderDTO>> GetPagedOrderAsync(int pageNumber, int pageSize, string? searchTerm , int statusId , int paymentId);
    }
}
