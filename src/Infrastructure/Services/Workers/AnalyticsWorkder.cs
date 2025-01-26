
using Domain.Entities;

using Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Workers;

public class AnalyticsWorker(
    IServiceProvider serviceProvider,
    ILogger<AnalyticsWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Analytics Worker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var date = DateTime.UtcNow;

                await UpdateAnalytics(dbContext, date);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating analytics.");
            }

            // Run every 24 hours
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }

    private async Task UpdateAnalytics(AppDbContext dbContext, DateTime date)
    {
        var startDate = new DateTime(date.Year, date.Month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        // Fetch data for the current month
        var totalSales = await dbContext.Orders
            .Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate)
            .SumAsync(o => o.TotalAmount);

        var totalOrders = await dbContext.Orders
            .Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate)
            .CountAsync();

        var totalProducts = await dbContext.Products.CountAsync();
        var totalUsers = await dbContext.Users.CountAsync();

        var mostSoldProduct = await dbContext.OrderItems
            .Where(od => od.Order.CreatedAt >= startDate && od.Order.CreatedAt <= endDate)
            .GroupBy(od => od.ProductId)
            .OrderByDescending(g => g.Sum(od => od.Quantity))
            .Select(g => new { ProductId = g.Key, Quantity = g.Sum(od => od.Quantity) })
            .FirstOrDefaultAsync();

        var mostSoldProductId = mostSoldProduct?.ProductId ?? Guid.Empty;

        // Update existing record or create a new one
        var analytics = await dbContext.Analytics
            .FirstOrDefaultAsync(a => a.Date.Month == date.Month && a.Date.Year == date.Year);

        if (analytics == null)
        {
            analytics = new Analytics
            {
                Id = Guid.NewGuid(),
                Date = date,
                TotalSales = totalSales,
                TotalOrders = totalOrders,
                TotalProducts = totalProducts,
                TotalUsers = totalUsers,
                MostSoldProductId = mostSoldProductId,
                TotalRevenue = totalSales, // Assuming revenue is equivalent to total sales
                // TotalProfit = totalSales * 0.2m // Example: Assuming a 20% profit margin
            };

            await dbContext.Analytics.AddAsync(analytics);
        }
        else
        {
            analytics.TotalSales = totalSales;
            analytics.TotalOrders = totalOrders;
            analytics.TotalProducts = totalProducts;
            analytics.TotalUsers = totalUsers;
            analytics.MostSoldProductId = mostSoldProductId;
            analytics.TotalRevenue = totalSales;
            // analytics.TotalProfit = totalSales * 0.2m;
        }

        await dbContext.SaveChangesAsync();
    }
}