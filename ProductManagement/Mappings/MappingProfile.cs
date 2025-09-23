using AutoMapper;
using ProductManagement.Models;
using ProductManagement.DTOs;

namespace ProductManagement.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductDTO, Product>()
            .ReverseMap();
        CreateMap<Product, ProductDTO>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));
        CreateMap<ProductReview, ProductReviewDTO>()
            .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.User != null ? src.User.AvatarUrl : null));
        CreateMap<ProductReviewDTO, ProductReview>();
        CreateMap<CartItem, CartItemDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
            .ReverseMap();
        CreateMap<Cart, CartDTO>()
           .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems))
           .ReverseMap()
           .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Items));
        CreateMap<OrderDTO, Order>()
           .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
           .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.Now))
           .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.Now))
           .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(_ => DateTime.Now));

        CreateMap<OrderDTO, OrderItem>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.Now));
         CreateMap<OrderCreateDTO, Order>()
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<OrderItemCreateDTO, OrderItem>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<Order, OrderDTO>()
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus!.Name))
            .ForMember(dest => dest.PaymentName, opt => opt.MapFrom(src => src.Payment!.PaymentMethod))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
        CreateMap<OrderItem, OrderItemDTO>();
        CreateMap<Order, OrderResponseDTO>()
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus != null ? src.OrderStatus.Name : null))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.Payment!.PaymentMethod));
        CreateMap<OrderItem, OrderItemResponseDTO>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name));
        CreateMap<UserRegisterDTO, User>()
           .ForMember(dest => dest.HashedPassword, opt => opt.Ignore());
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.RoleName , opt => opt.MapFrom(src => src.Role != null? src.Role.Name : null))
            .ReverseMap();
        CreateMap<StockTransactionDTO, StockTransaction>().ReverseMap();
        CreateMap<Voucher, VoucherDTO>()
            //.ForMember(dest => dest.UserVouchers, opt => opt.MapFrom(src => src.UserVouchers))
            .ReverseMap();
        CreateMap<UserVoucher, UserVoucherDTO>().ReverseMap();
        CreateMap<ChatRoom, ChatRoomDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.LastName : null))
            .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src => src.Admin != null ? src.Admin.LastName : null))
            .ReverseMap();
        CreateMap<ChatMessage, ChatMessageDTO>()
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender != null ? src.Sender.LastName : null))
            .ReverseMap();
    }
}

