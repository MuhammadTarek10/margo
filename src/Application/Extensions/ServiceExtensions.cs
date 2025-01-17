using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Application.Services.Validator;
using Application.Services.Payment;
using Application.Services.Payement;

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
        service.AddScoped<IPaymentService, StripePaymentGateway>();

        service.AddHttpContextAccessor();
    }
}