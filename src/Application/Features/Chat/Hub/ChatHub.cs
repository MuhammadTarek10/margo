
using Application.Features.DTOs;

using Domain.Constants;

using Microsoft.AspNetCore.SignalR;

namespace Application.Features;

public class ChatHub : Hub
{
    public async Task SendChatMessage(ChatDto chatDto, MessageDto messageDto)
    {
        string user = Context.User?.Identity?.Name ?? throw new Exception("User is not authenticated.");
        string sendTo;

        if (user == chatDto.AgentName) sendTo = chatDto.CustomerName;
        else sendTo = chatDto.AgentName;

        await Clients.Users(sendTo).SendAsync(HubMessages.ReceiveMessage, messageDto.Content);
    }
}