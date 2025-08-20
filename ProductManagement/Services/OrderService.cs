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

        public OrderService(IOrderRepository orderRepository, ICartService cartService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
            _mapper = mapper;
        }

        public async Task<List<OrderResponseDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return _mapper.Map<List<OrderResponseDTO>>(orders);
        }

        public async Task<OrderResponseDTO?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            return order == null ? null : _mapper.Map<OrderResponseDTO>(order);
        }

        public async Task<List<OrderResponseDTO>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<List<OrderResponseDTO>>(orders);
        }

        public async Task<OrderResponseDTO> CreateOrderAsync(OrderCreateDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            // Tính tổng tiền đơn hàng
            decimal total = 0;
            foreach (var item in order.OrderItems)
            {
                // Lấy giá sản phẩm từ DB hoặc truyền từ DTO (tùy nghiệp vụ)
                // Giả sử có thuộc tính Price trong OrderItem
                total += item.Price * item.Quantity;
            }
            order.TotalPrice = total;
            // Tạo Payment mới
            var payment = new Payment
            {
                Amount = total,
                PaymentMethod = orderDto.PaymentMethod,
                PaymentDate = DateTime.Now,
            };
            await _orderRepository.CreatePaymentAsync(payment);
            order.PaymentId = payment.PaymentId;

            var createdOrder = await _orderRepository.CreateOrderAsync(order);
            return _mapper.Map<OrderResponseDTO>(createdOrder);
        }

        public async Task<OrderResponseDTO?> UpdateOrderStatusAsync(int orderId, OrderUpdateDTO updateDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
                return null;

            order.OrderStatusId = updateDto.OrderStatusId;
            var updatedOrder = await _orderRepository.UpdateOrderAsync(order);
            return _mapper.Map<OrderResponseDTO>(updatedOrder);
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }
        public async Task<OrderResponseDTO> CreateOrderFromCartAsync(int userId, OrderCreateDTO orderDto)
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

                // Tạo Payment mới
                var payment = new Payment
                {
                    Amount = order.TotalPrice,
                    PaymentMethod = orderDto.PaymentMethod,
                    PaymentDate = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await _orderRepository.CreatePaymentAsync(payment);
                order.PaymentId = payment.PaymentId;

                // Lưu order
                var createdOrder = await _orderRepository.CreateOrderAsync(order);

                // Xóa giỏ hàng sau khi tạo order thành công
                await _cartService.ClearCartAsync(userId);

                return _mapper.Map<OrderResponseDTO>(createdOrder);
            }
            else
            {
                // Nếu không có sản phẩm trong giỏ hàng, tạo order bình thường
                return await CreateOrderAsync(orderDto);
            }
        }
    }
}
