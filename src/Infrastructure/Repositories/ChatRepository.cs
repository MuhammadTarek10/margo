
using Application.Services.Auth;

using Domain.Entities;
using Domain.Interfaces;

using Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class ChatRepository(
    AppDbContext context,
    IUserContext userContext) : IChatRepository
{
    public async Task<Chat> CreateChatAsync(Guid userId)
    {
        Guid agentId = userContext.AvailableAgent;

        Console.WriteLine(agentId);

        Chat chat = new Chat { UserId = userId, AgentId = agentId };
        await context.Chats.AddAsync(chat);

        await context.SaveChangesAsync();
        return chat;
    }

    public Task<Chat> DeleteChatAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Chat> GetChatAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Chat>> GetChatsAsync(Guid userId)
    {
        Console.WriteLine(userId);
        return await context.Chats
            .Where(c => c.UserId == userId || c.AgentId == userId)
            .Include(c => c.User)
            .Include(c => c.Agent)
            .ToListAsync();
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