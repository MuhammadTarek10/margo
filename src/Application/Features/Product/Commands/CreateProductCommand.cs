using Application.DTOs;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Commands;

public class CreateProductCommand : IRequest<Guid>
{
    public required CreateProductDto ProductDto { get; set; }
}



public class CreateProductCommandHandler(IProductRepository repository) : IRequestHandler<CreateProductCommand, Guid>

{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.ProductDto.Name,
            Description = request.ProductDto.Description,
            Price = request.ProductDto.Price,
            Stock = request.ProductDto.Stock,
            Category = request.ProductDto.Category,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(product);

        return product.Id;
    }
}


public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal to 0.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(50).WithMessage("Category must not exceed 50 characters.");
    }
}
