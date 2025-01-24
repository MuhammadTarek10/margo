namespace Application.Services.Chat;


public interface IChatService
{
    Task SendChatMessage(string sendTo, string content);
}