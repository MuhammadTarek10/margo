using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Order
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal TotalAmount { get; set; }

    [Required]
    [MaxLength(50)]
    public OrderStatus Status { get; set; } // e.g., "Pending", "Paid", "Shipped", "Delivered", "Cancelled"

    [MaxLength(100)]
    public string? PaymentIntentId { get; set; } // Stripe Payment Intent ID

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public required User User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


    public enum OrderStatus
    {
        Pending,    // Order is created but payment is not yet completed
        Paid,       // Payment is completed
        Shipped,    // Order has been shipped
        Delivered,  // Order has been delivered to the customer
        Cancelled   // Order has been cancelled
    }
}

public class OrderItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }

    [Required]
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal PriceAtTimeOfOrder { get; set; } // Historical price

    // Navigation properties
    public required Order Order { get; set; }
    public required Product Product { get; set; }
}

