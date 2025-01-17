using Application.Features.Orders.Dtos;

using AutoMapper;

using Domain.Interfaces;

using MediatR;
using Application.Services.Auth;
using Domain.Entities;

namespace Application.Features.Orders.Queries;


public class GetMyOrdersQuery : IRequest<List<OrderDto>>
{
}

public class GetMyOrdersQueryHandler(
        IOrderRepository orderRepository,
        IMapper mapper,
        IUserContext userContext) : IRequestHandler<GetMyOrdersQuery, List<OrderDto>>
{
    public async Task<List<OrderDto>> Handle(GetMyOrdersQuery request, CancellationToken cancellationToken)
    {

        Guid userId = userContext.UserId;
        List<Order> orders = await orderRepository.GetOrdersByUserIdAsync(userId);

        return mapper.Map<List<OrderDto>>(orders);
    }
}