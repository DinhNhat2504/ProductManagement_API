using ProductManagement.DTOs;

namespace ProductManagement.Services
{
    public interface ICartService
    {
        Task<CartDTO> GetCartAsync(int userId);
        Task<bool> AddOrUpdateCartItemAsync(int userId, CartItemDTO dto);
        Task<bool> RemoveCartItemAsync(int userId, int productId);
        Task<bool> ClearCartAsync(int userId);
        Task<List<CartItemDTO>> GetCartItemsAsync(int cartId);
        Task<CartDTO> GetCartByIdAsync(int cartId);
        Task<bool> AddOrUpdateCartItemByCartIdAsync(int cartId, CartItemDTO dto);
        Task<bool> RemoveCartItemByCartIdAsync(int cartId, int productId);
        Task<bool> ClearCartByCartIdAsync(int cartId);
        Task<CartDTO> CreateGuestCartAsync();
    }
}
