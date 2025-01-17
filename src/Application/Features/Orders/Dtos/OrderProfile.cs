using AutoMapper;

using Domain.Entities;
namespace Application.Features.Orders.Dtos;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItemDto, OrderItem>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<Order, CreateOrderDto>();
        CreateMap<CreateOrderDto, Order>();
    }
}