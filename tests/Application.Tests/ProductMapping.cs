using Application.Features.Products.DTOs;
using Application.Features.DTOs;

using AutoMapper;

using Domain.Entities;


public class ProductProfileTests
{
    private readonly IMapper _mapper;

    public ProductProfileTests()
    {
        // Configure AutoMapper with the ProductProfile
        var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Map_Product_To_ProductDto()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Laptop",
            Description = "High-performance laptop",
            Price = 1200.00m,
            Stock = 10,
            Category = "Electronics",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Act
        var productDto = _mapper.Map<ProductDto>(product);

        // Assert
        Assert.Equal(product.Id, productDto.Id);
        Assert.Equal(product.Name, productDto.Name);
        Assert.Equal(product.Description, productDto.Description);
        Assert.Equal(product.Price, productDto.Price);
        Assert.Equal(product.Stock, productDto.Stock);
        Assert.Equal(product.Category, productDto.Category);
    }

    [Fact]
    public void Map_Product_To_CreateProductDto()
    {
        // Arrange
        var product = new Product
        {
            Name = "Laptop",
            Description = "High-performance laptop",
            Price = 1200.00m,
            Stock = 10,
            Category = "Electronics"
        };

        // Act
        var createProductDto = _mapper.Map<CreateProductDto>(product);

        // Assert
        Assert.Equal(product.Name, createProductDto.Name);
        Assert.Equal(product.Description, createProductDto.Description);
        Assert.Equal(product.Price, createProductDto.Price);
        Assert.Equal(product.Stock, createProductDto.Stock);
        Assert.Equal(product.Category, createProductDto.Category);
    }

    [Fact]
    public void Map_Product_To_UpdateProductDto()
    {
        // Arrange
        var product = new Product
        {
            Name = "Laptop",
            Description = "High-performance laptop",
            Price = 1200.00m,
            Stock = 10,
            Category = "Electronics"
        };

        // Act
        var updateProductDto = _mapper.Map<UpdateProductDto>(product);

        // Assert
        Assert.Equal(product.Name, updateProductDto.Name);
        Assert.Equal(product.Description, updateProductDto.Description);
        Assert.Equal(product.Price, updateProductDto.Price);
        Assert.Equal(product.Stock, updateProductDto.Stock);
        Assert.Equal(product.Category, updateProductDto.Category);
    }

    [Fact]
    public void Map_CreateProductDto_To_Product()
    {
        // Arrange
        var createProductDto = new CreateProductDto
        {
            Name = "Laptop",
            Description = "High-performance laptop",
            Price = 1200.00m,
            Stock = 10,
            Category = "Electronics"
        };

        // Act
        var product = _mapper.Map<Product>(createProductDto);

        // Assert
        Assert.Equal(createProductDto.Name, product.Name);
        Assert.Equal(createProductDto.Description, product.Description);
        Assert.Equal(createProductDto.Price, product.Price);
        Assert.Equal(createProductDto.Stock, product.Stock);
        Assert.Equal(createProductDto.Category, product.Category);
    }

    [Fact]
    public void Map_UpdateProductDto_To_Product()
    {
        // Arrange
        var updateProductDto = new UpdateProductDto
        {
            Name = "Laptop",
            Description = "High-performance laptop",
            Price = 1200.00m,
            Stock = 10,
            Category = "Electronics"
        };

        // Act
        var product = _mapper.Map<Product>(updateProductDto);

        // Assert
        Assert.Equal(updateProductDto.Name, product.Name);
        Assert.Equal(updateProductDto.Description, product.Description);
        Assert.Equal(updateProductDto.Price, product.Price);
        Assert.Equal(updateProductDto.Stock, product.Stock);
        Assert.Equal(updateProductDto.Category, product.Category);
    }
}