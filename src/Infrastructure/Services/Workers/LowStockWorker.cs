using Application.Services.Notifications;

using Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Workers;

public class LowStockNotifierWorker(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<LowStockNotifierWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("LowStockNotifierWorker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                // Threshold for low stock
                const int lowStockThreshold = 4;

                // Fetch low-stock products
                var lowStockProducts = await dbContext.Products
                    .Where(p => p.Stock < lowStockThreshold)
                    .ToListAsync(stoppingToken);

                if (lowStockProducts.Any())
                {
                    logger.LogInformation("Found {Count} low-stock products.", lowStockProducts.Count);

                    foreach (var product in lowStockProducts)
                    {
                        // Notify admin about the low-stock product
                        await notificationService.NotifiyAdminsAsync("Low Stock", $"Product {product.Name} is low on stock (Quantity: {product.Stock})");
                    }
                }
                else
                {
                    logger.LogInformation("No low-stock products found.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while checking for low-stock products.");
            }

            // Run the worker again after 24 hours
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}