using Domain.Entities;

namespace Application.Services.Payement;

public interface IPaymentService
{

    Task<PaymentResult> ProcessPaymentAsync(decimal amount, string currency, string description, string email);
}