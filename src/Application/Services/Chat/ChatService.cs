using Application.Features;

using Microsoft.AspNetCore.SignalR;

namespace Application.Services.Chat;

internal class ChatService(IHubContext<ChatHub, IChatSender> hubContext) : IChatService
{
    public async Task SendChatMessage(string chatId, string sendTo, string content)
    {
        // await hubContext.Clients
        //     .Users(sendTo.ToString())
        //     .SendAsync(HubMessages.ReceiveMessage, new
        //     {
        //         Content = content,
        //         SenderId = sendTo,
        //         Timestamp = DateTime.UtcNow
        //     });
        //
        await hubContext
            .Clients
            .Groups(chatId)
            .SendChatMessage(sendTo, content);
    }

}