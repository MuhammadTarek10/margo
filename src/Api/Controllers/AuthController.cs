using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Features.Auth.DTOs;

[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator, ILogger<AuthController> logger) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid) return BadRequest(new { success = false, message = "Invalid registration data." });

        var command = new RegisterUserCommand { RegisterDto = registerDto };
        var userId = await mediator.Send(command);

        logger.LogInformation($"User registered successfully with ID: {userId}");
        return Ok(new { success = true, userId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest(new { success = false, message = "Invalid login data." });

        var command = new LoginUserCommand { LoginDto = loginDto };
        var token = await mediator.Send(command);

        logger.LogInformation("User logged in successfully.");
        return Ok(new { success = true, token });
    }
}