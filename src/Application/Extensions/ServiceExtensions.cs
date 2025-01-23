using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Application.Services.Validator;
using Application.Services.Payment;
using Application.Services.Payement;
using Application.Services.Email;
using Application.Services.Notifications;
using Application.Events.Notifications;

namespace Application.Extentions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection service)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        service.AddAutoMapper(assembly);
        service.AddSignalR();

        service.AddValidatorsFromAssembly(assembly)
                  .AddFluentValidationAutoValidation();

        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        service.AddScoped<IPaymentService, StripePaymentGateway>();
        service.AddScoped<IEmailService, EmailService>();
        service.AddScoped<INotificationService, NotificationService>();
        service.AddTransient<INotificationHandler<OrderCreatedEvent>, OrderCreatedEventHandler>();


        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        service.AddHttpContextAccessor();
    }
}