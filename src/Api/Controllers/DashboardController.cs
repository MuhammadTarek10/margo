
using Application.Features.Queries;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/dashboard")]
public class DashboardController(IMediator mediator) : ControllerBase

{
    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetAnalytics()
    {
        var analytics = await mediator.Send(new GetAnalyticsQuery());
        return Ok(new { success = true, data = analytics });
    }

}