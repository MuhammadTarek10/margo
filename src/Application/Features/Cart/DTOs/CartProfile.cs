using Application.Features.DTOs;

using AutoMapper;

using Domain.Entities;

public class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        CreateMap<Cart, CartDto>()
            .ForMember(dest => dest.items, opt => opt.MapFrom(src => src.CartItems))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.CartItems.Sum(ci => ci.Product!.Price * ci.Quantity)));

        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product!.Price));
    }
}