
using Domain.Entities;

namespace Domain.Interfaces;

public interface IDashboardRepository
{
    Task<Analytics> GetCurrentAnalytics();
    Task<Analytics> GetAnalyticsByDate(DateTime date);
    Task<List<Analytics>> GetAnalyticsByDateRange(DateTime startDate, DateTime endDate);
}