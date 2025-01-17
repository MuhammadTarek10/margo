using Application.Features.Orders.Dtos;

using AutoMapper;

using Domain.Entities;

public class OrderMappingProfileTests
{
    private readonly IMapper _mapper;

    public OrderMappingProfileTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<OrderProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Map_CreateOrderDto_To_Order()
    {
        // Arrange
        var createOrderDto = new CreateOrderDto
        {
            UserId = Guid.NewGuid(),
            Items = new List<OrderItemDto>
            {
                new OrderItemDto { ProductId = Guid.NewGuid(), Quantity = 2 }
            }
        };

        // Act
        var order = _mapper.Map<Order>(createOrderDto);

        // Assert
        Assert.Equal(createOrderDto.UserId, order.UserId);
        Assert.Equal(createOrderDto.Items.Count, order.Items.Count);
    }
}