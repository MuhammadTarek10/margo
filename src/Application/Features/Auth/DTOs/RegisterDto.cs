namespace Application.Features.Auth.DTOs;

public class RegisterDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
}