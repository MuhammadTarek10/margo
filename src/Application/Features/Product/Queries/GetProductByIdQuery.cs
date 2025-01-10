using Domain.Entities;
using Domain.Exceptions;
using MediatR;

public class GetProductByIdQuery : IRequest<Product>
{
    public Guid Id { get; set; }
}

public class GetProductByIdQueryHandler(IProductRepository repository) : IRequestHandler<GetProductByIdQuery, Product>
{

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id);
        if (product is null) throw new NotFoundException(nameof(Product), request.Id.ToString());

        return product;
    }
}
