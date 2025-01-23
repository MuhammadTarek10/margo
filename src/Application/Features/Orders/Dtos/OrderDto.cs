using Domain.Constants;
namespace Application.Features.Orders.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<OrderItemDto> Items { get; set; } = [];


    public OrderStatus GetStatus(string status)
    {
        return status switch
        {
            "Pending" => OrderStatus.Pending,
            "Paid" => OrderStatus.Paid,
            "Failed" => OrderStatus.Failed,
            "Shipped" => OrderStatus.Shipped,
            "Delivered" => OrderStatus.Delivered,
            "Cancelled" => OrderStatus.Cancelled,
            _ => OrderStatus.Pending
        };
    }
}