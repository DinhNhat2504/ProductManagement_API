using AutoMapper;
using ProductManagement.DTOs;
using ProductManagement.Models;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddOrUpdateCartItemAsync(int userId, CartItemDTO dto)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId) ?? await _cartRepository.CreateCartAsync(userId);
            var cartItem = _mapper.Map<CartItem>(dto);
            return await _cartRepository.AddOrUpdateCartItemAsync(cart.CartId, cartItem.ProductId, cartItem.Quantity);
        }

        public async Task<bool> ClearCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null) return false;
            return await _cartRepository.ClearCartAsync(cart.CartId);

        }

        public async Task<CartDTO> GetCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = await _cartRepository.CreateCartAsync(userId);
            }
            return _mapper.Map<CartDTO>(cart);
        }

        public async Task<List<CartItemDTO>> GetCartItemsAsync(int cartId)
        {
            var items = await _cartRepository.GetCartItemsAsync(cartId);
            return _mapper.Map<List<CartItemDTO>>(items);

        }

        public async Task<bool> RemoveCartItemAsync(int userId, int productId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null) return false;
            return await _cartRepository.RemoveCartItemAsync(cart.CartId, productId);
        }
        public async Task<CartDTO> GetCartByIdAsync(int cartId)
        {
            var cart = await _cartRepository.GetCartByIdAsync(cartId);
            return _mapper.Map<CartDTO>(cart);
        }
        public async Task<bool> AddOrUpdateCartItemByCartIdAsync(int cartId, CartItemDTO dto)
        {
            var cartItem = _mapper.Map<CartItem>(dto);
            return await _cartRepository.AddOrUpdateCartItemByCartIdAsync(cartId, cartItem.ProductId, cartItem.Quantity);
        }
        public async Task<bool> RemoveCartItemByCartIdAsync(int cartId, int productId)
        {
            return await _cartRepository.RemoveCartItemByCartIdAsync(cartId, productId);
        }
        public async Task<bool> ClearCartByCartIdAsync(int cartId)
        {
            return await _cartRepository.ClearCartByCartIdAsync(cartId);
        }
        public async Task<CartDTO> CreateGuestCartAsync()
        {
            var cart = await _cartRepository.CreateGuestCartAsync();
            return _mapper.Map<CartDTO>(cart);
        }
    }
}
