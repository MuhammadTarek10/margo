using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Constants;

namespace Domain.Entities;

public class Payment
{
    public Payment()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Status = PaymentStatus.Pending;
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid OrderId { get; set; } // Foreign key to the Order entity

    [Required]
    public PaymentMethod Method { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Amount { get; set; }

    public PaymentStatus Status { get; set; }

    [StringLength(100)]
    public string? TransactionId { get; set; } // Gateway-specific transaction ID (e.g., Stripe charge ID)

    [Column(TypeName = "jsonb")]
    public string? PaymentDetails { get; set; } // Store gateway-specific details as JSON

    // Navigation property to Order
    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }

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
        return $"Payment ID: {Id}, Amount: {Amount}, Method: {GetPaymentMethodDescription()}, Status: {Status}";
    }
}