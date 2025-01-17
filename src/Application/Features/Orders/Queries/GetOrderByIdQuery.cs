using Application.Features.Orders.Dtos;

using AutoMapper;

using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

using MediatR;

namespace Application.Features.Orders.Queries;

public class GetOrderByIdQuery : IRequest<OrderDto>
{
    public Guid OrderId { get; set; }
}

public class GetOrderByIdQueryHandler(
        IMapper mapper,
    IOrderRepository orderRepository) : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the order
        var order = await orderRepository.GetByIdAsync(request.OrderId);
        if (order is null) throw new NotFoundException(nameof(Order), request.OrderId.ToString());

        return mapper.Map<OrderDto>(order);
    }
}