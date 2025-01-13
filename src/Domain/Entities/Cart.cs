using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Cart
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}

public class CartItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid CartId { get; set; }

    [Required]
    public Guid ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    // Navigation properties
    [ForeignKey(nameof(CartId))]
    public Cart? Cart { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }
}
