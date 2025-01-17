using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Order
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal TotalAmount { get; set; }

    [Required]
    public OrderStatus Status { get; set; } // e.g., "Pending", "Paid", "Shipped", "Delivered", "Cancelled"

    [MaxLength(100)]
    public string? PaymentIntentId { get; set; } // Stripe Payment Intent ID

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();


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
    public Guid OrderId { get; set; }

    [Required]
    public Guid ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal PriceAtTimeOfOrder { get; set; } // Historical price

    // Navigation properties
    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }
}
