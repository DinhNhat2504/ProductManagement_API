using ProductManagement.DTOs;

namespace ProductManagement.Services
{
    public interface IOrderService
    {
        Task<List<OrderResponseDTO>> GetAllOrdersAsync();
        Task<OrderResponseDTO?> GetOrderByIdAsync(int orderId);
        Task<List<OrderResponseDTO>> GetOrdersByUserIdAsync(int userId);
        Task<OrderResponseDTO> CreateOrderAsync(OrderCreateDTO orderDto);
        Task<OrderResponseDTO?> UpdateOrderStatusAsync(int orderId, OrderUpdateDTO updateDto);
        Task<bool> DeleteOrderAsync(int orderId);
        Task<OrderResponseDTO> CreateOrderFromCartAsync(int userId, OrderCreateDTO orderDto);
    }
}
