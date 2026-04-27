using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll(CancellationToken cancellationToken);
        Task<Order> GetById(Guid id, CancellationToken cancellationToken);
        Task Add(Order order, CancellationToken cancellationToken);
        Task Delete(Order order, CancellationToken cancellationToken);
    }
}
