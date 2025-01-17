using Application.Features.DTOs;
using Application.Services.Auth;

using AutoMapper;

using Domain.Exceptions;
using Domain.Interfaces;

using MediatR;

namespace Application.Features.Queries;

public class GetCartQuery : IRequest<CartDto>
{
}

public class GetCartQueryHandler(
    IMapper mapper,
    IUserContext userContext,
    ICartRepository cartRepository) : IRequestHandler<GetCartQuery, CartDto>
{

    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        Guid userId = userContext.UserId;

        var cart = await cartRepository.GetCartByUserIdAsync(userId);
        if (cart is null) throw new NotFoundException(nameof(cart), userId.ToString());

        return mapper.Map<CartDto>(cart);
    }
}