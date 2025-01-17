using Application.Featuers.Commands;
using Application.Features.Cart.Queries;
using Application.Features.Commands;
using Application.Features.DTOs;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController(IMediator mediator) : ControllerBase
{

    // GET: api/cart
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var result = await mediator.Send(new GetCartQuery());
        return Ok(result);
    }

    // POST: api/cart/add-product
    [Authorize]
    [HttpPost("add-product")]
    public async Task<IActionResult> AddItem([FromBody] AddToCartDto dto)
    {
        var result = await mediator.Send(new AddProductToCartCommand { CartDto = dto });
        return Ok(new { ItemId = result });
    }

    // DELETE: api/cart/remove-product
    [Authorize]
    [HttpDelete("remove-product")]
    public async Task<IActionResult> RemoveItem([FromBody] Guid productId)
    {
        await mediator.Send(new RemoveProductFromCartCommand { ProductId = productId });
        return NoContent();
    }
}