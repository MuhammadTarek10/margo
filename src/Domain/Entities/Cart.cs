using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Cart
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public required User User { get; set; }

    [ForeignKey(nameof(UserId))]
    public Guid UserId { get; set; }

    public List<CartItem> CartItems { get; set; } = [];
}

public class CartItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public required Cart Cart { get; set; }

    [Required]
    [ForeignKey(nameof(CartId))]
    public Guid CartId { get; set; }

    [Required]
    public required Product Product { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Guid ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
