using Domain.Entities;

namespace Application.Services.Token;

public interface ITokenService
{
    Task<string> GenerateToken(User user);
}