using Domain.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{

    public async Task<Order> GetByIdAsync(Guid id)
    {
        return await context.Orders
            .Include(o => o.Items)
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == id) ?? throw new NotFoundException(nameof(Order), id.ToString());
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await context.Orders
            .Include(o => o.Items)
            .Include(o => o.User)
            .ToListAsync();
    }

    public async Task AddAsync(Order order)
    {
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await context.Orders.FindAsync(id);
        if (order != null)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId)
    {
        return await context.Orders
            .Include(o => o.Items)
            .Include(o => o.User)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }
}