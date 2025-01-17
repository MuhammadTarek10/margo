
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

using Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class NotificationRepository(AppDbContext context) : INotificationRepository
{
    public async Task AddAsync(Notification notification)
    {
        await context.Notifications.AddAsync(notification);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Notification notification)
    {
        context.Notifications.Remove(notification);
        await context.SaveChangesAsync();
    }

    public async Task<List<Notification>> GetAllAsync()
    {
        return await context.Notifications.ToListAsync();
    }

    public async Task<Notification> GetNotificationByIdAndMessage(Guid id, string message)
    {
        return await context.Notifications
            .FirstOrDefaultAsync(n => n.RecipientId == id && n.Message == message) ?? throw new NotFoundException(nameof(Notification), id.ToString());
    }

    public async Task<Notification?> GetNotificationByIdAsync(Guid id)
    {
        return await context.Notifications
            .FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<List<Notification>> GetNotificationsByUserIdAsync(Guid userId)
    {
        return await context.Notifications
            .Where(n => n.UserId == userId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Notification notification)
    {
        context.Notifications.Update(notification);
        await context.SaveChangesAsync();
    }
}