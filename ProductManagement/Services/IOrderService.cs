using ProductManagement.DTOs;

namespace ProductManagement.Services
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(Guid orderId);
        Task<List<OrderResponseDTO>> GetOrdersByUserIdAsync(int userId);
        Task<OrderDTO> CreateOrderAsync(OrderCreateDTO orderDto);
        Task<OrderDTO?> UpdateOrderStatusAsync(Guid orderId, OrderUpdateDTO updateDto);
        Task<bool> DeleteOrderAsync(Guid orderId);
        Task<OrderDTO> CreateOrderFromCartAsync(int userId, OrderCreateDTO orderDto);
        Task<PagedResult<OrderDTO>> GetPagedOrderAsync(int pageNumber, int pageSize, string? searchTerm , int statusId , int paymentId);
    }
}
