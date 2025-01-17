using Application.Featuers.Products.DTOs;
using Application.Features.Commands;
using Application.Features.DTOs;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    // GET: api/products
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var query = new GetAllProductsQuery();
        var products = await mediator.Send(query);
        return Ok(products);
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var query = new GetProductByIdQuery { Id = id };
        var product = await mediator.Send(query);
        return Ok(product);
    }

    // GET: api/products/category/{category}
    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetProductsByCategory(string category)
    {
        var query = new GetProductsByCategoryQuery { Category = category };
        var products = await mediator.Send(query);
        return Ok(products);
    }

    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        var command = new CreateProductCommand { ProductDto = productDto };
        var productId = await mediator.Send(command);
        return Ok(productId);
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto productDto)
    {
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
        return NoContent();
    }
}