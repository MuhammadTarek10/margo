using Application.Features.Products.DTOs;

using AutoMapper;

using Domain.Entities;

using MediatR;

public class GetAllProductsQuery : IRequest<List<ProductDto>>
{
    public string? category { get; set; }
    public bool? stock { get; set; }
    public decimal? lowerPrice { get; set; }
    public decimal? higherPrice { get; set; }
}

public class GetAllProductsQueryHandler(
    IMapper mapper,
    IProductRepository repository) : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {

        List<Product> products = await repository.GetAllAsync(request.category, request.stock, request.lowerPrice, request.higherPrice);

        return mapper.Map<List<ProductDto>>(products);
    }
}