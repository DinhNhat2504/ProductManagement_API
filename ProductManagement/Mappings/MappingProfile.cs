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
    }
}

