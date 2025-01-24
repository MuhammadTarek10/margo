using Application.Features.Commands;
using Application.Features.Orders.Queries;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/orders")]
public class OrderController(IMediator mediator) : ControllerBase
{
    // GET: api/orders
    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {

        var query = new GetAllOrdersQuery();
        var orders = await mediator.Send(query);
        return Ok(new { success = true, data = orders });
    }

    // GET: api/orders/{id}
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {

        var query = new GetOrderByIdQuery { OrderId = id };
        var order = await mediator.Send(query);

        if (order == null) return NotFound(new { success = false, message = "Order not found." });

        return Ok(new { success = true, data = order });
    }

    // POST: api/orders
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {

        var orderId = await mediator.Send(new CreateOrderCommand());
        return Ok(new { success = true, data = orderId });
    }

    // GET: api/orders/mine
    [Authorize]
    [HttpGet("mine")]
    public async Task<IActionResult> GetMyOrders()
    {

        var query = new GetMyOrdersQuery();
        var orders = await mediator.Send(query);
        return Ok(new { success = true, data = orders });
    }

    // DELETE: api/orders/{id}
    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var command = new DeleteOrderCommand { OrderId = id };
        await mediator.Send(command);
        return Ok(new { success = true, message = "Order deleted successfully." });
    }
}