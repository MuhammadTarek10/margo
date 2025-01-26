using Domain.Exceptions;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class ProductRepository(AppDbContext context) : IProductRepository
{

    public async Task<Product> GetByIdAsync(Guid id)
    {
        Product? product = await context.Products.FindAsync(id);

        if (product is null) throw new NotFoundException(nameof(Product), id.ToString());

        return product;
    }

    public async Task<List<Product>> GetAllAsync(
        string? category,
        bool? stock,
        decimal? lowerPrice,
        decimal? higherPrice)
    {

        return await context.Products
            .Where(p => category == null || p.Category == null ? true : p.Category.ToLower() == category.ToLower())
            .Where(p => stock == null || stock == true ? p.Stock > 0 : p.Stock == 0)
            .Where(p => lowerPrice == null || p.Price >= lowerPrice)
            .Where(p => higherPrice == null || p.Price <= higherPrice)
            .ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if (product != null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}