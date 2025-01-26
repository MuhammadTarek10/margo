namespace Application.Features;

public interface IChatSender
{
    Task SendChatMessage(string sendTo, string content);

}

internal class ChatSender : IChatSender
{
    public Task SendChatMessage(string sendTo, string content)
    {
        throw new NotImplementedException();
    }
}