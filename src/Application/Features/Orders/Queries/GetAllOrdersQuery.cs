using Application.Features.Orders.Dtos;

using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using MediatR;

namespace Application.Features.Orders.Queries;

public class GetAllOrdersQuery : IRequest<List<OrderDto>>
{
}
public class GetAllOrdersQueryHandler(
    IMapper mapper,
    IOrderRepository orderRepository) : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        List<Order> orders = await orderRepository.GetAllAsync();
        return mapper.Map<List<OrderDto>>(orders);
    }
}