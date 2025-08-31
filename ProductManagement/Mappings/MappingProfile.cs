using AutoMapper;
using ProductManagement.Models;
using ProductManagement.DTOs;

namespace ProductManagement.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductDTO, Product>().ReverseMap();
        CreateMap<ProductReview, ProductReviewDTO>()
            .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.User != null ? src.User.AvatarUrl : null));
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
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
        CreateMap<OrderItem, OrderItemDTO>();

        CreateMap<OrderItem, OrderItemResponseDTO>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name));
        CreateMap<UserRegisterDTO, User>()
           .ForMember(dest => dest.HashedPassword, opt => opt.Ignore());
        CreateMap<User, UserDTO>();
    }
}

