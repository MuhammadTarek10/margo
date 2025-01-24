

using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

using Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class DashboardRepository(AppDbContext context) : IDashboardRepository
{
    public async Task<Analytics> GetAnalyticsByDate(DateTime date)
    {
        Analytics? analytics = await context.Analytics
            .FirstOrDefaultAsync(a => a.Date.Month == date.Month);

        if (analytics is null) throw new NotFoundException(nameof(Analytics), date.Month.ToString());

        return analytics;
    }

    public async Task<List<Analytics>> GetAnalyticsByDateRange(DateTime startDate, DateTime endDate)
    {
        List<Analytics> analytics = await context.Analytics
            .Where(a => a.Date >= startDate && a.Date <= endDate)
            .ToListAsync();

        return analytics;
    }

    public async Task<Analytics> GetCurrentAnalytics()
    {
        Analytics? analytics = await context.Analytics
            .FirstOrDefaultAsync(a => a.Date.Month == DateTime.Now.Month);

        if (analytics is null) throw new NotFoundException(nameof(Analytics), DateTime.Now.Month.ToString());

        return analytics;
    }
}