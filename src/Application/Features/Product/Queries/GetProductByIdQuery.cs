using Application.Featuers.Products.DTOs;

using AutoMapper;

using Domain.Entities;
using Domain.Exceptions;

using MediatR;

public class GetProductByIdQuery : IRequest<ProductDto>
{
    public Guid Id { get; set; }
}

public class GetProductByIdQueryHandler(
    IMapper mapper,
    IProductRepository repository) : IRequestHandler<GetProductByIdQuery, ProductDto>
{

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product? product = await repository.GetByIdAsync(request.Id);
        if (product is null) throw new NotFoundException(nameof(Product), request.Id.ToString());

        return mapper.Map<ProductDto>(product);
    }
}