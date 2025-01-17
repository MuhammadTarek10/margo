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
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid registration data." });

            var command = new RegisterUserCommand { RegisterDto = registerDto };
            var userId = await mediator.Send(command);

            logger.LogInformation($"User registered successfully with ID: {userId}");
            return Ok(new { success = true, userId });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during user registration");
            return StatusCode(500, new { success = false, message = "An error occurred during registration." });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid login data." });

            var command = new LoginUserCommand { LoginDto = loginDto };
            var token = await mediator.Send(command);

            logger.LogInformation("User logged in successfully.");
            return Ok(new { success = true, token });
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogWarning($"Unauthorized login attempt: {ex.Message}");
            return Unauthorized(new { success = false, message = "Invalid credentials." });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during login");
            return StatusCode(500, new { success = false, message = "An error occurred during login." });
        }
    }
}