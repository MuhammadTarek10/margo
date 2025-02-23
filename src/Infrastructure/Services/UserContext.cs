using System.Security.Claims;

using Application.Services.Auth;

using Domain.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Auth;

internal class UserContext(IHttpContextAccessor http, UserManager<User> userManager) : IUserContext
{
    public Guid UserId => Guid.Parse(http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public string Email => http.HttpContext?.User?.FindFirstValue(ClaimTypes.Email)!;

    public List<NotificationData>? Admins => GetAdmins().Result;

    public Guid AvailableAgent => GetAvailableAgent().Result;

    private async Task<List<NotificationData>> GetAdmins()
    {
        var admins = await userManager.GetUsersInRoleAsync(Roles.Admin);

        return admins.Select(user => new NotificationData(
            user.Id,
            user.Email!)).Where(user => user.email is not null)
            .ToList();
    }

    private async Task<Guid> GetAvailableAgent()
    {
        var agents = await userManager.GetUsersInRoleAsync(Roles.Agent);

        if (agents.Count == 0) throw new Exception("No agents available");

        return agents[new Random().Next(0, agents.Count)].Id;
    }

}