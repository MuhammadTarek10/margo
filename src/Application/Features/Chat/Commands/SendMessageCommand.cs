using Application.Features.DTOs;
using Application.Services.Auth;
using Application.Services.Chat;

using AutoMapper;

using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

using MediatR;

namespace Application.Features.Commands;

public class SendMessageCommand : IRequest<ChatDto>
{
    public required MessageDto Dto { get; set; }
}

public class SendMessageCommandHandler(
    IMapper mapper,
    IUserContext userContext,
    IChatRepository chatRepository,
    IChatService chatService) : IRequestHandler<SendMessageCommand, ChatDto>
{
    public async Task<ChatDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {

        Chat chat = await chatRepository.GetChatAsync(request.Dto.ChatId);
        if (chat is null) throw new NotFoundException(nameof(Chat), request.Dto.ChatId.ToString());

        Guid userId = userContext.UserId;

        request.Dto.SenderId = userId;
        request.Dto.RecieverId = userId == chat.UserId ? chat.AgentId : chat.UserId;

        Message message = mapper.Map<Message>(request.Dto);

        await chatRepository.SendMessageAsync(request.Dto.ChatId, message);

        ChatDto chatDto = mapper.Map<ChatDto>(chat);

        await chatService.SendChatMessage(request.Dto.RecieverId.ToString(), request.Dto.Content);

        return chatDto;
    }
}