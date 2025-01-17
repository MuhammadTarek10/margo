using MediatR;
using Domain.Entities;
using Domain.Interfaces;
using Application.Features.Orders.Dtos;
using AutoMapper;

namespace Application.Features.Commands;

public class CreateOrderCommand : IRequest<Guid>
{
    public required CreateOrderDto OrderDto { get; set; }
}

public class CreateOrderCommandHandler(
    IMapper mapper,
    IOrderRepository orderRepository,
    IProductRepository productRepository) : IRequestHandler<CreateOrderCommand, Guid>
{

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {

        Order order = mapper.Map<Order>(request.OrderDto);
        order.Status = Order.OrderStatus.Pending;
        order.TotalAmount = await CalculateTotalAmount(request.OrderDto.Items);

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