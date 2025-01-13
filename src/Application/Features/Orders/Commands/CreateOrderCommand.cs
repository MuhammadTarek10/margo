using MediatR;
using Domain.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using Application.Features.Orders.Dtos;

namespace Application.Features.Orders.Commands;


public class CreateOrderCommand : IRequest<Guid>
{
    public required CreateOrderDto OrderDto { get; set; }
}

public class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IProductRepository productRepository) : IRequestHandler<CreateOrderCommand, Guid>
{

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            TotalAmount = await CalculateTotalAmount(request.OrderDto.Items),
            Status = Order.OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            OrderItems = request.OrderDto.Items.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            }).ToList()
        };


        await orderRepository.AddAsync(order);

        return order.Id;
    }

    private async Task<decimal> CalculateTotalAmount(List<OrderItemDto> items)
    {
        decimal totalAmount = 0;

        foreach (var item in items)
        {
            Product product = await productRepository.GetByIdAsync(item.ProductId);

            totalAmount += product.Price * item.Quantity;
        }

        return totalAmount;
    }
}
