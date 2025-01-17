using Application.Features.DTOs;
using Application.Services.Auth;

using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

using MediatR;

namespace Application.Features.Commands;

public class AddProductToCartCommand : IRequest<Guid>
{
    public required AddToCartDto CartDto { get; set; }
}

public class AddProductToCartCommandHandler(
    IUserContext userContext,
    IProductRepository productRepository,
    ICartRepository cartRepository) : IRequestHandler<AddProductToCartCommand, Guid>
{

    public async Task<Guid> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
    {
        Guid userId = userContext.UserId;

        var product = await productRepository.GetByIdAsync(request.CartDto.ProductId);
        if (product is null) throw new NotFoundException(nameof(product), request.CartDto.ProductId.ToString());


        var cart = await cartRepository.GetCartByUserIdAsync(userId);
        if (cart is null)
        {
            cart = new Cart { UserId = userId };
            await cartRepository.AddAsync(cart);
        }

        var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == request.CartDto.ProductId);
        if (cartItem is null)
        {
            cartItem = new CartItem
            {
                CartId = request.CartDto.Id,
                ProductId = request.CartDto.ProductId,
                Quantity = request.CartDto.Quantity
            };
            cart.Items.Add(cartItem);
        }
        else
        {
            cartItem.Quantity += request.CartDto.Quantity;
        }

        await cartRepository.UpdateAsync(cart);
        return cartItem.Id;
    }
}