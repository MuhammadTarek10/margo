using MediatR;
using Domain.Entities;
using Domain.Interfaces;
using AutoMapper;
using Application.Services.Auth;
using Domain.Exceptions;
using Application.Services.Payement;

namespace Application.Features.Commands;

public class CreateOrderCommand : IRequest<Guid>
{
}

public class CreateOrderCommandHandler(
    IUserContext userContext,
    IMapper mapper,
    ICartRepository cartRepository,
    IOrderRepository orderRepository,
    IPaymentService paymentService) : IRequestHandler<CreateOrderCommand, Guid>
{

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Guid userId = userContext.UserId;
        string email = userContext.Email;

        Cart? cart = await cartRepository.GetCartByUserIdAsync(userId);

        if (cart is null) throw new NotFoundException(nameof(Cart), userId.ToString());

        if (cart.Items.Count == 0) throw new BadRequestException("Cart is empty");

        Order order = mapper.Map<Order>(cart);
        order.Status = Order.OrderStatus.Pending;

        PaymentResult result = await paymentService.ProcessPaymentAsync(order.TotalAmount, "egp", $"Order {order.Id}", email);

        order.Status = result.Success ? Order.OrderStatus.Paid : Order.OrderStatus.Failed;
        order.TransactionId = result.TransactionId;
        order.PaymentDetails = result.Success ? "Stripe payment successful" : result.ErrorMessage;

        if (result.Success) await cartRepository.ClearCartAsync(userId);

        Console.WriteLine($"Result: {result.Success}, TransactionId: {result.TransactionId}, ErrorMessage: {result.ErrorMessage}");

        // Update the Order status
        order.Status = result.Success ? Order.OrderStatus.Paid : Order.OrderStatus.Failed;

        await orderRepository.AddAsync(order);
        return order.Id;
    }
}