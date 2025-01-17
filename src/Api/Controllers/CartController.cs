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
        return Ok(result);
    }

    // POST: api/cart/add-product
    [HttpPost("add-product")]
    public async Task<IActionResult> AddItem([FromBody] AddToCartDto dto)
    {
        Guid result = await mediator.Send(new AddProductToCartCommand { CartDto = dto });
        return Ok(new { result });
    }

    // DELETE: api/cart/remove-product
    [HttpDelete("remove-product")]
    public async Task<IActionResult> RemoveItem([FromBody] Guid productId)
    {
        await mediator.Send(new RemoveProductFromCartCommand { ProductId = productId });
        return NoContent();
    }


}