using Domain.Entities;

namespace Domain.Interfaces;

public interface IChatRepository
{
    Task<List<Chat>> GetChatsAsync();
    Task<Chat> GetChatAsync(Guid id);
    Task<Chat> CreateChatAsync(Chat chat);
    Task<Chat> UpdateChatAsync(Chat chat);
    Task<Chat> DeleteChatAsync(Guid id);
    Task<Chat> SendMessageAsync(Guid chatId, Message message);
}