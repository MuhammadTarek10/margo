using Application.Features.Orders.Dtos;
using MediatR;

namespace Application.Features.Orders.Queries;

public class GetOrderByUserIdQuery : IRequest<OrderDto>
{
    public Guid OrderId { get; set; }
}
