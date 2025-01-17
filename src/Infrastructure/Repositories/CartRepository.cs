using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

using Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class CartRepository(AppDbContext context) : ICartRepository
{
    public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
    {
        var cart = await context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart is null)
        {
            cart = new Cart { UserId = userId };
            await context.Carts.AddAsync(cart);
            await context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task AddAsync(Cart cart)
    {
        await context.Carts.AddAsync(cart);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cart cart)
    {
        context.Carts.Update(cart);
        await context.SaveChangesAsync();
    }

    public async Task RemoveProductFromCartAsync(Guid userId, Guid productId)
    {
        var cart = await context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart is null) throw new NotFoundException(nameof(Cart), userId.ToString());

        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (cartItem is null) throw new NotFoundException(nameof(CartItem), productId.ToString());

        cart.CartItems.Remove(cartItem);
        await context.SaveChangesAsync();
    }
}