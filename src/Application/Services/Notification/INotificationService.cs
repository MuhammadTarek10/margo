using Application.Services.Auth;

namespace Application.Services.Notifications;

public interface INotificationService
{
    Task SendAsync(List<NotificationData> userIds, string subject, string message);
    Task NotifiyAdminsAsync(string subject, string message);
}