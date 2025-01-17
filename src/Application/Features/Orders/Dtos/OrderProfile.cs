using AutoMapper;

using Domain.Entities;
namespace Application.Features.Orders.Dtos;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.GetStatusDescription()));

        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.GetStatus(src.Status ?? "Pending")));

        CreateMap<OrderItemDto, OrderItem>();
        CreateMap<OrderItem, OrderItemDto>();

        CreateMap<Cart, Order>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Items.Sum(item => item.Product!.Price * item.Quantity)));
        CreateMap<CartItem, OrderItem>();
    }
}