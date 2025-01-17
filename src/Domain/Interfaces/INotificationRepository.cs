
using Domain.Entities;

namespace Domain.Interfaces;

public interface INotificationRepository
{
    Task AddAsync(Notification notification);
    Task<List<Notification>> GetAllAsync();
    Task<Notification?> GetNotificationByIdAsync(Guid id);
    Task UpdateAsync(Notification notification);
    Task DeleteAsync(Notification notification);
    Task<List<Notification>> GetNotificationsByUserIdAsync(Guid userId);
    Task<Notification> GetNotificationByIdAndMessage(Guid id, string message);
}