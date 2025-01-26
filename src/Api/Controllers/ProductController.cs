using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Products.DTOs;
using Application.Features.Commands;
using Application.Features.DTOs;


namespace Api.Controllers;



[ApiController]
[Authorize(Roles = Roles.Admin)]
[Route("api/products")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    // GET: api/products
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllProducts(
        [FromQuery] string? category,
        [FromQuery] bool? stock,
        [FromQuery] decimal? lowerPrice,
        [FromQuery] decimal? higherPrice)
    {
        var query = new GetAllProductsQuery
        {
            category = category,
            stock = stock,
            lowerPrice = lowerPrice,
            higherPrice = higherPrice
        };
        var products = await mediator.Send(query);
        return Ok(new { success = true, data = products });
    }

    // GET: api/products/{id}
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var query = new GetProductByIdQuery { Id = id };
        var product = await mediator.Send(query);
        if (product == null)
            return NotFound(new { success = false, message = "Product not found." });

        return Ok(new { success = true, data = product });
    }


    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        if (!ModelState.IsValid) return BadRequest(new { success = false, message = "Invalid product data." });

        var command = new CreateProductCommand { ProductDto = productDto };
        var productId = await mediator.Send(command);
        return Ok(new { success = true, data = productId });
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { success = false, message = "Invalid product data." });

        var command = new UpdateProductCommand
        {
            Id = id,
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Stock = productDto.Stock,
            Category = productDto.Category
        };

        await mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var command = new DeleteProductCommand { Id = id };
        await mediator.Send(command);
        return Ok(new { success = true, message = "Product deleted successfully." });
    }
}