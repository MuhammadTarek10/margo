using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Constants;

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

    public string GetStatusDescription()
    {
        return Status switch
        {
            OrderStatus.Pending => "Pending",
            OrderStatus.Paid => "Paid",
            OrderStatus.Failed => "Failed",
            OrderStatus.Shipped => "Shipped",
            OrderStatus.Delivered => "Delivered",
            OrderStatus.Cancelled => "Cancelled",
            _ => "Unknown"
        };
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
    public required Order Order { get; set; }

    [ForeignKey(nameof(ProductId))]
    public required Product Product { get; set; }
}


public class PaymentResult
{
    public bool Success { get; set; }
    public string? TransactionId { get; set; }
    public string? PaymentDetails { get; set; }
    public string? ErrorMessage { get; set; }
}