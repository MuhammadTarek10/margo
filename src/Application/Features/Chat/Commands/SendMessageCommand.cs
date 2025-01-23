
using Application.Features.DTOs;
using Application.Services.Auth;

using AutoMapper;

using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

using MediatR;

using Microsoft.AspNetCore.SignalR;

namespace Application.Features.Commands;

public class SendMessageCommand : IRequest<ChatDto>
{
    public required MessageDto Dto { get; set; }
}

public class SendMessageCommandHandler(
    IMapper mapper,
    IUserContext userContext,
    IChatRepository chatRepository,
    IHubContext<ChatHub> chatHub) : IRequestHandler<SendMessageCommand, ChatDto>
{
    public async Task<ChatDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        Chat chat = await chatRepository.GetChatAsync(request.Dto.ChatId);
        if (chat is null) throw new NotFoundException(nameof(Chat), request.Dto.ChatId.ToString());

        Guid userId = userContext.UserId;

        request.Dto.SenderId = userId;
        if (userId == chat.UserId) request.Dto.RecieverId = chat.AgentId;
        else request.Dto.RecieverId = chat.UserId;

        Message message = mapper.Map<Message>(request.Dto);


        await chatRepository.SendMessageAsync(request.Dto.ChatId, message);

        ChatDto chatDto = mapper.Map<ChatDto>(chat);

        await chatHub.Clients
                        .Users(chatDto.AgentName, chatDto.CustomerName)
                        .SendAsync(HubMessages.ReceiveMessage, request.Dto.Content, cancellationToken);

        return mapper.Map<ChatDto>(chat);
    }
}