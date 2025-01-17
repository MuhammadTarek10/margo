using Application.Featuers.Products.DTOs;

using AutoMapper;

using Domain.Entities;

using MediatR;

public class GetAllProductsQuery : IRequest<List<ProductDto>>
{
}

public class GetAllProductsQueryHandler(
    IMapper mapper,
    IProductRepository repository) : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        List<Product> products = await repository.GetAllAsync();
        return mapper.Map<List<ProductDto>>(products);
    }
}