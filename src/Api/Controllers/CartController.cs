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
public class CartController(IMediator mediator) : ControllerBase
{
    // GET: api/cart
    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var result = await mediator.Send(new GetCartQuery());
        return Ok(new { success = true, data = result });
    }

    // POST: api/cart/product
    [HttpPost("product")]
    public async Task<IActionResult> AddItem([FromBody] AddToCartDto dto)
    {

        Guid result = await mediator.Send(new AddProductToCartCommand { CartDto = dto });
        return Ok(new { success = true, data = result });
    }

    // DELETE: api/cart/product
    [HttpDelete("product/{id}")]
    public async Task<IActionResult> RemoveItem(Guid id)
    {

        await mediator.Send(new RemoveProductFromCartCommand { ProductId = id });
        return Ok(new { success = true, message = "Product removed from cart successfully." });
    }
}