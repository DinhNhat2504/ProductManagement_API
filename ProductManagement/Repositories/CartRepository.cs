using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Cart> CreateCartAsync(int userId)
        {
            var cart = new Cart { UserId = userId, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<bool> AddOrUpdateCartItemAsync(int cartId, int productId, int quantity)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = cartId,
                    ProductId = productId,
                    Quantity = quantity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity = quantity;
                cartItem.UpdatedAt = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCartItemAsync(int cartId, int productId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
            if (cartItem == null) return false;
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCartAsync(int cartId)
        {
            var items = _context.CartItems.Where(ci => ci.CartId == cartId);
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CartItem>> GetCartItemsAsync(int cartId)
        {
            return await _context.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();
        }

        public async Task<Cart?> GetCartByIdAsync(int cartId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CartId == cartId);
        }

        public async Task<Cart> CreateGuestCartAsync()
        {
            var cart = new Cart
            {
                UserId = null, // Không liên kết với user
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<bool> AddOrUpdateCartItemByCartIdAsync(int cartId, int productId, int quantity)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = cartId,
                    ProductId = productId,
                    Quantity = quantity,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity = quantity;
                cartItem.UpdatedAt = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCartItemByCartIdAsync(int cartId, int productId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
            if (cartItem == null) return false;
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCartByCartIdAsync(int cartId)
        {
            var items = _context.CartItems.Where(ci => ci.CartId == cartId);
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
