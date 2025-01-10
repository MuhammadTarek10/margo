using Domain.Exceptions;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{

    public async Task<Product> GetByIdAsync(Guid id)
    {
        Product? product = await context.Products.FindAsync(id);

        if (product is null) throw new NotFoundException(nameof(Product), id.ToString());

        return product;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await context.Products.ToListAsync();
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

    public async Task<List<Product>> GetByCategoryAsync(string category)
    {
        return await context.Products.Where(p => p.Category == category).ToListAsync();
    }
}
