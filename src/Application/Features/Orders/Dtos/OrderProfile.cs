using AutoMapper;

using Domain.Entities;
namespace Application.Features.Orders.Dtos;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<OrderDto, Order>();

        CreateMap<OrderItemDto, OrderItem>();
        CreateMap<OrderItem, OrderItemDto>();

        CreateMap<Cart, Order>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Items.Sum(item => item.Product!.Price * item.Quantity)));
        CreateMap<CartItem, OrderItem>();
    }
}