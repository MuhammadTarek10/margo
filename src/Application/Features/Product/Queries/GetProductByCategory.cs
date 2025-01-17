using Application.Features.Products.DTOs;

using AutoMapper;

using MediatR;

public class GetProductsByCategoryQuery : IRequest<List<ProductDto>>
{
    public required string Category { get; set; }
}

public class GetProductsByCategoryQueryHandler(
    IMapper mapper,
    IProductRepository repository) : IRequestHandler<GetProductsByCategoryQuery, List<ProductDto>>
{

    public async Task<List<ProductDto>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await repository.GetByCategoryAsync(request.Category);
        return mapper.Map<List<ProductDto>>(products);
    }
}