using MediatR;

public class DeleteProductCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeleteProductCommandHandler(IProductRepository repository) : IRequestHandler<DeleteProductCommand>
{

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new Exception("Product not found.");
        }

        await repository.DeleteAsync(request.Id);
    }
}
