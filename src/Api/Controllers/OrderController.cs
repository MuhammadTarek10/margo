using Application.Features.Commands;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Queries;

using MediatR;

using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
    {
        var query = new GetOrderByIdQuery { OrderId = id };
        var order = await mediator.Send(query);
        return Ok(order);
    }

    // POST: api/orders
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateOrder()
    {
        var orderId = await mediator.Send(new CreateOrderCommand());
        return Ok(orderId);
    }

    // GET: api/orders/mine
    [Authorize]
    [HttpGet("mine")]
    public async Task<ActionResult<List<OrderDto>>> GetMyOrders()
    {
        var query = new GetMyOrdersQuery();
        var orders = await mediator.Send(query);
        return Ok(orders);
    }

    // DELETE: api/orders/{id}
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(Guid id)
    {
        var command = new DeleteOrderCommand { OrderId = id };
        await mediator.Send(command);
        return NoContent();
    }
}