using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(int userId);
        Task<Cart> CreateCartAsync(int userId);
        Task<bool> AddOrUpdateCartItemAsync(int cartId, int productId, int quantity);
        Task<bool> UpdateQuantityItem(int cartId, int productId, int quantity);
        Task<bool> RemoveCartItemAsync(int cartId, int productId);
        Task<bool> ClearCartAsync(int cartId);
        Task<List<CartItem>> GetCartItemsAsync(int cartId);
        Task<Cart?> GetCartByIdAsync(int cartId);
    }
}
