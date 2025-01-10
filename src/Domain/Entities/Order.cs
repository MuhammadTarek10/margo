using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Order
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public required User User { get; set; }

    [Required]
    [ForeignKey(nameof(UserId))]
    public Guid UserId { get; set; }

    [Required]
    public List<OrderItem> OrderItems { get; set; } = [];

    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class OrderItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public required Order Order { get; set; }  // Navigation property to Order

    [Required]
    [ForeignKey(nameof(OrderId))]
    public Guid OrderId { get; set; }

    [Required]
    public required Product Product { get; set; }  // Navigation property to Product

    [Required]
    [ForeignKey(nameof(ProductId))]
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
