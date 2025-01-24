using MediatR;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Application.Features.Auth.DTOs;
using Domain.Exceptions;
using Application.Services.Token;

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
        var user = await userManager.FindByEmailAsync(request.LoginDto.Email);

        if (user == null || !await userManager.CheckPasswordAsync(user, request.LoginDto.Password))
        {
            throw new AuthException("Invalid email or password.");
        }

        string token = await tokenService.GenerateToken(user);

        return token;
    }
}