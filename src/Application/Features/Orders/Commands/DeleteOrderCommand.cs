using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

using MediatR;

namespace Application.Features.Orders.Commands;

public class DeleteOrderCommand : IRequest
{
    public Guid OrderId { get; set; }
}

public class DeleteOrderCommandHandler(
    IOrderRepository orderRepository) : IRequestHandler<DeleteOrderCommand>
{
    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the order
        Order order = await orderRepository.GetByIdAsync(request.OrderId);
        if (order is null) throw new NotFoundException(nameof(Order), request.OrderId.ToString());

        // Delete the order
        await orderRepository.DeleteAsync(request.OrderId);
    }
}