using Domain.Entities;
using MediatR;

public class GetAllProductsQuery : IRequest<List<Product>>
{
}

public class GetAllProductsQueryHandler(IProductRepository repository) : IRequestHandler<GetAllProductsQuery, List<Product>>
{

    public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync();
    }
}
