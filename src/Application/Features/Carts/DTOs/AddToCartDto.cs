
namespace Application.Features.DTOs;

public class AddToCartDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}