using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Products.DTOs;
using Application.Features.Commands;
using Application.Features.DTOs;


namespace Api.Controllers;



[Authorize(Roles = Roles.Admin)]
[ApiController]
[Route("api/products")]
public class ProductsController(IMediator mediator, ILogger<ProductsController> logger) : ControllerBase
{
    // GET: api/products
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var query = new GetAllProductsQuery();
            var products = await mediator.Send(query);
            return Ok(new { success = true, data = products });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving all products");
            return StatusCode(500, new { success = false, message = "An error occurred while retrieving products." });
        }
    }

    // GET: api/products/{id}
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        try
        {
            var query = new GetProductByIdQuery { Id = id };
            var product = await mediator.Send(query);
            if (product == null)
                return NotFound(new { success = false, message = "Product not found." });

            return Ok(new { success = true, data = product });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error retrieving product with ID {id}");
            return StatusCode(500, new { success = false, message = "An error occurred while retrieving the product." });
        }
    }

    // GET: api/products/category/{category}
    [AllowAnonymous]
    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetProductsByCategory(string category)
    {
        try
        {
            var query = new GetProductsByCategoryQuery { Category = category };
            var products = await mediator.Send(query);
            return Ok(new { success = true, data = products });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error retrieving products in category {category}");
            return StatusCode(500, new { success = false, message = "An error occurred while retrieving products." });
        }
    }

    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid product data." });

            var command = new CreateProductCommand { ProductDto = productDto };
            var productId = await mediator.Send(command);
            return Ok(new { success = true, productId });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating product");
            return StatusCode(500, new { success = false, message = "An error occurred while creating the product." });
        }
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto productDto)
    {
        try
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
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error updating product with ID {id}");
            return StatusCode(500, new { success = false, message = "An error occurred while updating the product." });
        }
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var command = new DeleteProductCommand { Id = id };
            await mediator.Send(command);
            return Ok(new { success = true, message = "Product deleted successfully." });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error deleting product with ID {id}");
            return StatusCode(500, new { success = false, message = "An error occurred while deleting the product." });
        }
    }
}
