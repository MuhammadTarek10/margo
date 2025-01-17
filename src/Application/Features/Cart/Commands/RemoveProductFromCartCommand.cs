using Application.Services.Auth;

using Domain.Interfaces;

using MediatR;

namespace Application.Features.Commands;

public class RemoveProductFromCartCommand : IRequest
{
    public Guid ProductId { get; set; }
}

public class RemoveProductFromCartCommandHandler(
    IUserContext userContext,
    ICartRepository cartRepository) : IRequestHandler<RemoveProductFromCartCommand>
{
    public Task Handle(RemoveProductFromCartCommand request, CancellationToken cancellationToken)
    {
        Guid userId = userContext.UserId;
        return cartRepository.RemoveProductFromCartAsync(userId, request.ProductId);
    }
}