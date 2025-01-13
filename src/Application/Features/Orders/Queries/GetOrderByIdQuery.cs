using Application.Features.Orders.Dtos;
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
    IOrderRepository orderRepository) : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the order
        var order = await orderRepository.GetByIdAsync(request.OrderId);
        if (order is null) throw new NotFoundException(nameof(Order), request.OrderId.ToString());

        // Map the order to a DTO
        return new OrderDto
        {
            Id = order.Id,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt,
            Items = order.OrderItems.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            }).ToList()
        };
    }
}
