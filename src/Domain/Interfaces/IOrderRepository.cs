using Domain.Entities;

namespace Domain.Interfaces;


public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid id);
    Task<List<Order>> GetAllAsync();
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Guid id);
    Task<List<Order>> GetOrdersByUserIdAsync(Guid userId);
}
