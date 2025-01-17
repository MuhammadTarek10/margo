using Application.Services.Notifications;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.Events.Notifications;

public class OrderCreatedEvent : INotification
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
}

public class OrderCreatedEventHandler(
    INotificationService notificationService,
    ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            // Create a notification message
            var message = $"New order created: Order ID {notification.OrderId}, Total Amount {notification.TotalAmount}";

            // Notify admins
            await notificationService.NotifiyAdminsAsync(
                $"New order created: Order ID {notification.OrderId}, Total Amount {notification.TotalAmount}",
                message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to handle OrderCreatedEvent.");
            throw;
        }
    }
}