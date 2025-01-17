namespace Application.Services.Auth;

public interface IUserContext
{
    Guid UserId { get; }
    string Email { get; }
}