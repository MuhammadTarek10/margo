using Application.Features.Orders.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Orders.Queries;

public class GetAllOrdersQuery : IRequest<List<OrderDto>>
{
}
public class GetAllOrdersQueryHandler(
    IOrderRepository orderRepository) : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        // Retrieve all orders
        var orders = await orderRepository.GetAllAsync();

        // Map orders to DTOs
        return orders.Select(order => new OrderDto
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
        }).ToList();
    }
}
