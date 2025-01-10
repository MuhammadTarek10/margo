using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Analytics
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal TotalSales { get; set; } // Total revenue

    [Required]
    [Range(0, int.MaxValue)]
    public int TotalOrders { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int TotalUsers { get; set; }

    [Required]
    [ForeignKey(nameof(Product))]
    public Guid MostSoldProductId { get; set; }

    [Required]
    public DateTime Date { get; set; } // e.g., daily, weekly, or monthly analytics

    // Navigation properties
    public Product? MostSoldProduct { get; set; }
}
