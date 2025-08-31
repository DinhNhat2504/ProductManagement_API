using ProductManagement.DTOs;

namespace ProductManagement.Services
{
    public interface ICartService
    {
        Task<CartDTO> GetCartAsync(int userId);
        Task<bool> AddOrUpdateCartItemAsync(int userId, CartItemDTO dto);
        Task<bool> UpdateQuantityCart(int userId, CartItemDTO dto);
        Task<bool> RemoveCartItemAsync(int userId, int productId);
        Task<bool> ClearCartAsync(int userId);
        Task<List<CartItemDTO>> GetCartItemsAsync(int cartId);
        Task<CartDTO> GetCartByIdAsync(int cartId);

    }
}
