using Application.Features.DTOs;
using Application.Services.Auth;

using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using MediatR;

namespace Application.Features.Commands;

public class CreateChatCommand : IRequest<ChatDto>
{

}

public class CreateChatCommandHandler(
    IMapper mapper,
    IUserContext userContext,
    IChatRepository chatRepository) : IRequestHandler<CreateChatCommand, ChatDto>
{
    public async Task<ChatDto> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        Guid userId = userContext.UserId;

        Chat chat = await chatRepository.CreateChatAsync(userId);

        return mapper.Map<ChatDto>(chat);
    }
}

