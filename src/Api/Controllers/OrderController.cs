using Application.Features.Orders.Commands;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/orders")]
public class OrderController(IMediator mediator) : ControllerBase
{

    // GET: api/orders
    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
    {
        var query = new GetAllOrdersQuery();
        var orders = await mediator.Send(query);
        return Ok(orders);
    }

    // GET: api/orders/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
    {
        var query = new GetOrderByIdQuery { OrderId = id };
        var order = await mediator.Send(query);
        return Ok(order);
    }

    // POST: api/orders
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderDto orderDto)
    {
        var command = new CreateOrderCommand { OrderDto = orderDto };
        var orderId = await mediator.Send(command);
        return Ok(orderId);
    }

    // DELETE: api/orders/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(Guid id)
    {
        var command = new DeleteOrderCommand { OrderId = id };
        await mediator.Send(command);
        return NoContent();
    }
}
