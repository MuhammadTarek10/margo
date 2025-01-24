using Application.Features.Products.DTOs;

namespace Application.Features.DTOs;

public class AnalyticsDto
{
    public int TotalSales { get; set; }
    public int TotalOrders { get; set; }
    public int TotalProducts { get; set; }
    public int TotalUsers { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalProfit { get; set; }
    public DateTime Date { get; set; }
    public ProductDto? MostSoldProduct { get; set; }
}