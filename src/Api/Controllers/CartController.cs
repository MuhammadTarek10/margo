using Application.Features.Commands;
using Application.Features.DTOs;
using Application.Features.Queries;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/cart")]
public class CartController(IMediator mediator, ILogger<CartController> logger) : ControllerBase
{
    // GET: api/cart
    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        try
        {
            var result = await mediator.Send(new GetCartQuery());
            return Ok(new { success = true, data = result });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving cart");
            return StatusCode(500, new { success = false, message = "An error occurred while retrieving the cart." });
        }
    }

    // POST: api/cart/add-product
    [HttpPost("add-product")]
    public async Task<IActionResult> AddItem([FromBody] AddToCartDto dto)
    {
        try
        {
            Guid result = await mediator.Send(new AddProductToCartCommand { CartDto = dto });
            return Ok(new { success = true, cartItemId = result });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding product to cart");
            return StatusCode(500, new { success = false, message = "An error occurred while adding the product to the cart." });
        }
    }

    // DELETE: api/cart/remove-product
    [HttpDelete("remove-product")]
    public async Task<IActionResult> RemoveItem([FromBody] Guid productId)
    {
        try
        {
            await mediator.Send(new RemoveProductFromCartCommand { ProductId = productId });
            return Ok(new { success = true, message = "Product removed from cart successfully." });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error removing product from cart");
            return StatusCode(500, new { success = false, message = "An error occurred while removing the product from the cart." });
        }
    }
}