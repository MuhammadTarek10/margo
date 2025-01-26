using Domain.Entities;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(Guid id);
    Task<List<Product>> GetAllAsync(
        string? category,
        bool? stock,
        decimal? lowerPrice,
        decimal? higherPrice);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);

}