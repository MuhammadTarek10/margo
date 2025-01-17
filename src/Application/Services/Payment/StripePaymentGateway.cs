

using Application.Services.Payement;

using Domain.Entities;

using Microsoft.Extensions.Configuration;

using Stripe;

namespace Application.Services.Payment;

public class StripePaymentGateway : IPaymentService
{
    private readonly string _stripeSecretKey;

    public StripePaymentGateway(IConfiguration configuration)
    {
        _stripeSecretKey = configuration["Stripe:SecretKey"]!;
        StripeConfiguration.ApiKey = _stripeSecretKey;
    }

    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string currency, string description, string customerEmail)
    {
        try
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Stripe expects amount in cents
                Currency = currency,
                Description = description,
                ReceiptEmail = customerEmail,
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            Console.WriteLine($"Payment intent created: {paymentIntent.Id}");

            return new PaymentResult
            {
                Success = true,
                TransactionId = paymentIntent.Id,
            };
        }
        catch (StripeException ex)
        {
            return new PaymentResult
            {
                Success = false,
                ErrorMessage = ex.Message,
            };
        }
    }
}