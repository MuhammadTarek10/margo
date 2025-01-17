namespace Application.Features.DTOs;

public class CartDto
{
    public Guid Id { get; set; }
    public List<CartItemDto> items { get; set; } = new();
    public decimal TotalPrice { get; set; }
}

public class CartItemDto
{
    public Guid Id { get; set; }
    public required string ProductName { get; set; }
    public required Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}