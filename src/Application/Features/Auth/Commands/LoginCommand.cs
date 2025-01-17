using MediatR;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Application.Services.Token;
using Application.Features.Auth.DTOs;

public class LoginUserCommand : IRequest<string>
{
    public required LoginDto LoginDto { get; set; }
}

public class LoginUserCommandHandler(
    UserManager<User> userManager,
    ITokenService tokenService) : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Find the user by email
        var user = await userManager.FindByEmailAsync(request.LoginDto.Email);

        // Check if the user exists and the password is correct
        if (user == null || !await userManager.CheckPasswordAsync(user, request.LoginDto.Password))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Generate a JWT token
        var token = tokenService.GenerateToken(user);

        return token;
    }
}