using MediatR;
using Application.Features.Auth.DTOs;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using AutoMapper;
using Domain.Exceptions;
using FluentValidation;

public class RegisterUserCommand : IRequest<Guid>
{
    public required RegisterDto RegisterDto { get; set; }
}

public class RegisterUserCommandHandler(
    UserManager<User> userManager,
    IMapper mapper) : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request.RegisterDto);

        var result = await userManager.CreateAsync(user, request.RegisterDto.Password);

        if (!result.Succeeded)
        {
            throw new AuthException("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return user.Id;
    }
}


public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches(@"[A-Z]").WithMessage("Passwords must have at least one uppercase letter ('A'-'Z').")
            .Matches(@"[a-z]").WithMessage("Passwords must have at least one lowercase letter ('a'-'z').")
            .Matches(@"\d").WithMessage("Passwords must have at least one numeric digit ('0'-'9').")
            .Matches(@"[\W_]").WithMessage("Passwords must have at least one non-alphanumeric character (e.g., '@', '#').");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords must match.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");



    }
}