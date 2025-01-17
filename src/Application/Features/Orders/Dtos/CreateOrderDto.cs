namespace Application.Features.Orders.Dtos;


public class CreateOrderDto
{
    public Guid UserId { get; set; }
    public List<OrderItemDto> Items { get; set; } = [];
}

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
