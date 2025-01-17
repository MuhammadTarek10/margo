
namespace Application.Services.Email;

internal class EmailService : IEmailService
{
    public async Task SendAsync(string to, string subject, string message)
    {
        Console.WriteLine($"Email sent to {to} with subject {subject} and message {message}");
    }
}
