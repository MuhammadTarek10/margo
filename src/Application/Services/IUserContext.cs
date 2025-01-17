using Domain.Entities;

namespace Application.Services.Auth;

public interface IUserContext
{
    Guid UserId { get; }
    string Email { get; }

    List<NotificationData>? Admins { get; }
}

public record NotificationData(Guid id, string email);