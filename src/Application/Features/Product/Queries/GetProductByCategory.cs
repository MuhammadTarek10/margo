using Domain.Entities;
using MediatR;

public class GetProductsByCategoryQuery : IRequest<List<Product>>
{
    public required string Category { get; set; }
}

public class GetProductsByCategoryQueryHandler(IProductRepository repository) : IRequestHandler<GetProductsByCategoryQuery, List<Product>>
{

    public async Task<List<Product>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetByCategoryAsync(request.Category);

    }
}

