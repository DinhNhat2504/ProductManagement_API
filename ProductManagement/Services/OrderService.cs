using AutoMapper;
using ProductManagement.DTOs;
using ProductManagement.Models;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;


        public OrderService(IOrderRepository orderRepository, ICartService cartService, IMapper mapper, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            return order == null ? null : _mapper.Map<OrderDTO>(order);
        }

        public async Task<List<OrderDTO>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> CreateOrderAsync(OrderCreateDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            decimal total = 0;
            foreach (var item in order.OrderItems)
            {
                // Lấy giá sản phẩm từ DB
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product != null)
                {
                    item.Price = product.Price;
                    total += item.Price * item.Quantity;
                }
                else
                {
                    item.Price = 0;
                }
            }
            order.TotalPrice = total;

            var createdOrder = await _orderRepository.CreateOrderAsync(order);
            return _mapper.Map<OrderDTO>(createdOrder);
        }

        public async Task<OrderDTO?> UpdateOrderStatusAsync(int orderId, OrderUpdateDTO updateDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
                return null;

            order.OrderStatusId = updateDto.OrderStatusId;
            var updatedOrder = await _orderRepository.UpdateOrderAsync(order);
            return _mapper.Map<OrderDTO>(updatedOrder);
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }
        public async Task<OrderDTO> CreateOrderFromCartAsync(int userId, OrderCreateDTO orderDto)
        {
            // Lấy giỏ hàng của user
            var cart = await _cartService.GetCartByIdAsync(userId);

            if (cart != null && cart.Items != null && cart.Items.Any())
            {
                // Chuyển sản phẩm từ cart sang order items
                orderDto.OrderItems = cart.Items.Select(ci => new OrderItemCreateDTO
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity
                }).ToList();

                // Tạo order mới từ cart items
                var order = _mapper.Map<Order>(orderDto);
                order.UserId = userId;

                // Tính tổng giá trị đơn hàng (giả sử có logic lấy giá sản phẩm từ DB)
                decimal total = 0;
                foreach (var item in order.OrderItems)
                {
                    // TODO: Lấy giá sản phẩm từ DB theo ProductId
                    // Ví dụ:
                     //var product = await _productRepository.GetByIdAsync(item.ProductId);
                     //item.Price = product.Price;
                     //total += item.Price * item.Quantity;

                    // Nếu chưa có logic lấy giá, tạm thời để giá = 0
                    item.Price = 0;
                }
                order.TotalPrice = total;

                // Lưu order
                var createdOrder = await _orderRepository.CreateOrderAsync(order);

                // Xóa giỏ hàng sau khi tạo order thành công
                await _cartService.ClearCartAsync(userId);

                return _mapper.Map<OrderDTO>(createdOrder);
            }
            else
            {
                // Nếu không có sản phẩm trong giỏ hàng, tạo order bình thường
                return await CreateOrderAsync(orderDto);
            }
        }

        public async Task<PagedResult<OrderDTO>> GetPagedOrderAsync(int pageNumber, int pageSize, string? searchTerm, int statusId, int paymentId)
        {
            var allOrders = string.IsNullOrEmpty(searchTerm)
                ? await _orderRepository.GetAllOrdersAsync()
                : await _orderRepository.SearchOrdersAsync(searchTerm);

            // Lọc theo statusId nếu được truyền vào
            if (statusId > 0)
                allOrders = allOrders.Where(o => o.OrderStatusId == statusId).ToList();

            // Lọc theo paymentId nếu được truyền vào
            if (paymentId > 0)
                allOrders = allOrders.Where(o => o.PaymentId == paymentId).ToList();

            var totalItems = allOrders.Count();
            var items = allOrders
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => _mapper.Map<OrderDTO>(p))
                .ToList();

            return new PagedResult<OrderDTO>
            {
                Items = items,
                TotalCount = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }
    }
}
