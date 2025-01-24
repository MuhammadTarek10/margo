using Application.Features;

using Domain.Constants;

using Microsoft.AspNetCore.SignalR;

namespace Application.Services.Chat;

internal class ChatService(IHubContext<ChatHub> hubContext) : IChatService
{
    public async Task SendChatMessage(string sendTo, string content)
    {
        await hubContext.Clients
            .Users(sendTo.ToString())
            .SendAsync(HubMessages.ReceiveMessage, new
            {
                Content = content,
                SenderId = sendTo,
                Timestamp = DateTime.UtcNow
            });
    }
}