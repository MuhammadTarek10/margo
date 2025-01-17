using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Application.Services.Token;
using MediatR;
using Application.Services.Validator;

namespace Application.Extentions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection service)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        service.AddAutoMapper(assembly);

        service.AddValidatorsFromAssembly(assembly)
                  .AddFluentValidationAutoValidation();

        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


        service.AddAuthorization();
        service.AddScoped<ITokenService, TokenService>();

        service.AddHttpContextAccessor();
    }
}