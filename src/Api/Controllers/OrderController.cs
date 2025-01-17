using Application.Features.Commands;
using Application.Features.Orders.Queries;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/orders")]
public class OrderController(IMediator mediator, ILogger<OrderController> logger) : ControllerBase
{
    // GET: api/orders
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            var query = new GetAllOrdersQuery();
            var orders = await mediator.Send(query);
            return Ok(new { success = true, data = orders });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving all orders");
            return StatusCode(500, new { success = false, message = "An error occurred while retrieving orders." });
        }
    }

    // GET: api/orders/{id}
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        try
        {
            var query = new GetOrderByIdQuery { OrderId = id };
            var order = await mediator.Send(query);
            if (order == null)
                return NotFound(new { success = false, message = "Order not found." });

            return Ok(new { success = true, data = order });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error retrieving order with ID {id}");
            return StatusCode(500, new { success = false, message = "An error occurred while retrieving the order." });
        }
    }

    // POST: api/orders
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {
        try
        {
            var orderId = await mediator.Send(new CreateOrderCommand());
            return Ok(new { success = true, orderId });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating order");
            return StatusCode(500, new { success = false, message = "An error occurred while creating the order." });
        }
    }

    // GET: api/orders/mine
    [Authorize]
    [HttpGet("mine")]
    public async Task<IActionResult> GetMyOrders()
    {
        try
        {
            var query = new GetMyOrdersQuery();
            var orders = await mediator.Send(query);
            return Ok(new { success = true, data = orders });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving user's orders");
            return StatusCode(500, new { success = false, message = "An error occurred while retrieving your orders." });
        }
    }

    // DELETE: api/orders/{id}
    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        try
        {
            var command = new DeleteOrderCommand { OrderId = id };
            await mediator.Send(command);
            return Ok(new { success = true, message = "Order deleted successfully." });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error deleting order with ID {id}");
            return StatusCode(500, new { success = false, message = "An error occurred while deleting the order." });
        }
    }
}
