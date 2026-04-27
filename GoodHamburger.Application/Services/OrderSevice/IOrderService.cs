using GoodHamburger.Application.Dtos;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Services.OrderSevice
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAll(CancellationToken cancellationToken);
        Task<Order> GetById(Guid id, CancellationToken cancellationToken);
        Task<Order> Add(List<OrderItemDto> items, CancellationToken cancellationToken);
        Task<Order> Update(UpdateOrderDTO order, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
