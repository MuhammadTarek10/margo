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
    public OrderStatus Status { get; set; } = OrderStatus.Pending; // Default to Pending

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Payment-related fields
    public PaymentMethod Method { get; set; } // e.g., CashOnDelivery, Card, Stripe, PayPal


    [StringLength(100)]
    public string? TransactionId { get; set; } // Gateway-specific transaction ID (e.g., Stripe charge ID)

    [Column(TypeName = "jsonb")]
    public string? PaymentDetails { get; set; } // Store gateway-specific details as JSON

    // Navigation properties
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    public enum OrderStatus
    {
        Pending,    // Order is created but payment is not yet completed
        Paid,       // Payment is completed
        Failed,     // Payment failed
        Shipped,    // Order has been shipped
        Delivered,  // Order has been delivered to the customer
        Cancelled   // Order has been cancelled
    }

    public enum PaymentMethod
    {
        CashOnDelivery,
        Card,
    }


    public string GetPaymentMethodDescription()
    {
        return Method switch
        {
            PaymentMethod.CashOnDelivery => "Cash on Delivery",
            PaymentMethod.Card => "Card Payment",
            _ => "Unknown"
        };
    }

    public override string ToString()
    {
        return $"Order ID: {Id}, Total Amount: {TotalAmount}, Status: {Status}, Payment Method: {GetPaymentMethodDescription()}";
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


public class PaymentResult
{
    public bool Success { get; set; }
    public string? TransactionId { get; set; }
    public string? PaymentDetails { get; set; }
    public string? ErrorMessage { get; set; }
}