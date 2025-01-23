
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

internal class ChatRepository : IChatRepository
{
    public Task<Chat> CreateChatAsync(Chat chat)
    {
        throw new NotImplementedException();
    }

    public Task<Chat> DeleteChatAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Chat> GetChatAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Chat>> GetChatsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Chat> SendMessageAsync(Guid chatId, Message message)
    {
        throw new NotImplementedException();
    }

    public Task<Chat> UpdateChatAsync(Chat chat)
    {
        throw new NotImplementedException();
    }
}