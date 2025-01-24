
using Application.Services.Auth;

using Domain.Entities;
using Domain.Exceptions;
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

        Chat chat = new Chat { UserId = userId, AgentId = agentId };
        await context.Chats.AddAsync(chat);

        await context.SaveChangesAsync();
        return chat;
    }

    public Task<Chat> DeleteChatAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Chat> GetChatAsync(Guid id)
    {
        return await context.Chats
            .Where(c => c.Id == id)
            .Include(c => c.User)
            .Include(c => c.Agent)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Chat), id.ToString());
    }

    public async Task<List<Chat>> GetChatsAsync(Guid userId)
    {
        return await context.Chats
            .Where(c => c.UserId == userId || c.AgentId == userId)
            .Include(c => c.User)
            .Include(c => c.Agent)
            .Include(c => c.Messages)
            .ToListAsync();
    }

    public async Task<Chat> SendMessageAsync(Guid chatId, Message message)
    {

        await context.Messages.AddAsync(message);

        await context.SaveChangesAsync();
        return await GetChatAsync(chatId);
    }

    public Task<Chat> UpdateChatAsync(Chat chat)
    {
        throw new NotImplementedException();
    }
}