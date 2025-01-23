
using Application.Features.DTOs;
using Application.Services.Auth;

using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using MediatR;

namespace Application.Features.Queries;

public class GetChatsQuery : IRequest<List<ChatDto>>
{
}

public class GetChatsQueryHandler(
    IMapper mapper,
    IUserContext userContext,
    IChatRepository chatRepository) : IRequestHandler<GetChatsQuery, List<ChatDto>>
{
    public async Task<List<ChatDto>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
    {
        Guid userId = userContext.UserId;

        List<Chat> chats = await chatRepository.GetChatsAsync(userId);

        return mapper.Map<List<ChatDto>>(chats);
    }
}