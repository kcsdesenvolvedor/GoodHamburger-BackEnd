using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Repositories;
using GoodHamburger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task Add(Order order, CancellationToken cancellationToken)
        {
            _context.Orders.Add(order);
        }

        public async Task Delete(Order order, CancellationToken cancellationToken)
        {
            _context.Remove(order);
        }

        public async Task<IEnumerable<Order>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ToListAsync(cancellationToken);
        }

        public async Task<Order> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken) ?? throw new KeyNotFoundException("Order not found");
        }
    }
}
