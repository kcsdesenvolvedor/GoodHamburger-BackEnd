using GoodHamburger.Application.Dtos;
using GoodHamburger.Application.Exceptions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Repositories;

namespace GoodHamburger.Application.Services.OrderSevice
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Add(List<OrderItemDto> itemsDto, CancellationToken cancellationToken)
        {
            var order = new Order();

            foreach (var item in itemsDto)
            {
                var itemMenu = MenuService.Items.FirstOrDefault(m => m.Type == item.Type)
                    ?? throw new ApplicationRuleException("Item inválido");

                var orderItem = new OrderItem(itemMenu.Type, itemMenu.Name, itemMenu.Price);
                order.AddItem(orderItem);
            }

            order.FinalizeOrder();
            await _orderRepository.Add(order, cancellationToken);
            return order;
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(id, cancellationToken);
            if (order == null)
                throw new NotFoundException("Order not found");

            await _orderRepository.Delete(order, cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetAll(CancellationToken cancellationToken)
        {
            return await _orderRepository.GetAll(cancellationToken);
        }

        public async Task<Order> GetById(Guid id, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(id, cancellationToken);
            if (order == null)
                throw new NotFoundException("Order not found");
            return order;
        }

        public async Task<Order> Update(UpdateOrderDTO dto, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(dto.OrderId, cancellationToken);

            if (order == null)
                throw new NotFoundException("Order not found");

            order.ClearItems();

            foreach (var item in dto.Items)
            {
                var menuItem = MenuService.Items
                    .FirstOrDefault(m => m.Type == item.Type && m.Name == item.Name)
                    ?? throw new ApplicationRuleException("Item inválido");

                order.AddItem(new OrderItem(menuItem.Type, menuItem.Name, menuItem.Price));
            }

            order.FinalizeOrder();

            return order;
        }
    }
}
