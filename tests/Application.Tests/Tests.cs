using Application.DTOs;
using Application.Features.Commands;
using Domain.Entities;
using Moq;

public class CreateProductCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Add_Product_To_Repository()
    {
        // Arrange
        var productRepositoryMock = new Mock<IProductRepository>();
        var handler = new CreateProductCommandHandler(productRepositoryMock.Object);

        var command = new CreateProductCommand
        {
            ProductDto = new CreateProductDto
            {
                Name = "Laptop",
                Description = "High-performance laptop",
                Price = 1200.00m,
                Stock = 10,
                Category = "Electronics"
            }
        };

        // Act
        var productId = await handler.Handle(command, CancellationToken.None);

        // Assert
        productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Return_Correct_ProductId()
    {
        // Arrange
        var productRepositoryMock = new Mock<IProductRepository>();
        var handler = new CreateProductCommandHandler(productRepositoryMock.Object);

        var command = new CreateProductCommand
        {
            ProductDto = new CreateProductDto
            {
                Name = "Laptop",
                Description = "High-performance laptop",
                Price = 1200.00m,
                Stock = 10,
                Category = "Electronics"
            }
        };

        // Mock the repository to return a specific product ID
        var expectedProductId = Guid.NewGuid();
        productRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>()))
                            .Callback<Product>(p => p.Id = expectedProductId);

        // Act
        var productId = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(expectedProductId, productId);
    }
}
