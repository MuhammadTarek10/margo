namespace Application.Services.Chat;


public interface IChatService
{
    Task SendChatMessage(string chatId, string sendTo, string content);
}