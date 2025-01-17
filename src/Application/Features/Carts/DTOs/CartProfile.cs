using Application.Features.DTOs;

using AutoMapper;

using Domain.Entities;

public class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        CreateMap<Cart, CartDto>()
            .ForMember(dest => dest.items, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Items.Sum(ci => ci.Product!.Price * ci.Quantity)));

        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product!.Id))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product!.Price));
    }
}