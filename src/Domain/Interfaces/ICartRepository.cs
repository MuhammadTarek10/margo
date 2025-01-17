using Domain.Entities;

namespace Domain.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetCartByUserIdAsync(Guid userId);
    Task AddAsync(Cart cart);
    Task UpdateAsync(Cart cart);
    Task RemoveProductFromCartAsync(Guid userId, Guid productId);
}