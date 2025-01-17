
using Application.Services.Auth;
using Application.Services.Email;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.Extensions.Logging;

namespace Application.Services.Notifications;


public class NotificationService(
    IUserContext userContext,
    INotificationRepository notificationRepository,
    IEmailService emailService,
    ILogger<NotificationService> logger) : INotificationService
{
    public async Task NotifiyAdminsAsync(string subject, string message)
    {
        List<NotificationData>? admins = userContext.Admins;

        logger.LogInformation("Admins: {Admins}", admins);


        if (admins is null) return;

        foreach (var admin in admins)
        {
            var notification = new Notification
            {
                Subject = subject,
                Message = message,
                RecipientId = admin.id,
                Status = Notification.NotificationStatus.Pending,
                UserId = userContext.UserId
            };

            await notificationRepository.AddAsync(notification);
        }

        await SendAsync(admins, subject, message);
    }

    public async Task SendAsync(List<NotificationData> data, string subject, string message)
    {
        foreach (NotificationData element in data)
        {
            try
            {
                await emailService.SendAsync(element.email, subject, message);
                var notification = await notificationRepository.GetNotificationByIdAndMessage(element.id, message);

                if (notification is null) continue;

                notification.Status = Notification.NotificationStatus.Sent;
                notification.SentAt = DateTime.Now;

                await notificationRepository.UpdateAsync(notification);
            }
            catch (Exception ex)
            {

                var notification = await notificationRepository.GetNotificationByIdAndMessage(element.id, message);
                notification.Status = Notification.NotificationStatus.Failed;
                await notificationRepository.UpdateAsync(notification);

                logger.LogError(ex, "Failed to send notification to {Email}", element.email);
            }
        }
    }
}