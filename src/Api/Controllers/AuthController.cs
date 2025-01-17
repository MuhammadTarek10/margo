using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Features.Auth.DTOs;

[ApiController]
[Route("api/auth")]
public class AuthController(
    IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var command = new RegisterUserCommand { RegisterDto = registerDto };
        var userId = await mediator.Send(command);

        return Ok(new { Message = "User registered successfully.", UserId = userId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var command = new LoginUserCommand { LoginDto = loginDto };
        var token = await mediator.Send(command);

        return Ok(new { Token = token });
    }
}