using System.Security.Claims;

using Application.Services.Auth;

using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Auth;

internal class UserContext(IHttpContextAccessor http) : IUserContext
{
    public Guid UserId => Guid.Parse(http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public string Email => http.HttpContext?.User?.FindFirstValue(ClaimTypes.Email)!;
}