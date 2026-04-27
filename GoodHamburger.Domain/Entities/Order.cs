using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        private readonly List<OrderItem> _items = new();
        public string ClientName { get; set; } // Apenas para identificação visual do pedido no frontend
        public IReadOnlyCollection<OrderItem> Items => _items;
        public decimal Subtotal { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public decimal Total { get; private set; }

        public void AddItem(OrderItem item)
        {
            if (_items.Any(i => i.Type == item.Type))
                throw new BusinessRuleException($"Item do tipo {item.Type} já foi adicionado.");

            _items.Add(item);
        }

        public void CalculateTotals()
        {
            Subtotal = _items.Sum(i => i.Price);

            var hasBurger = _items.Any(i => i.Type == Enums.MenuItemType.Burger);
            var hasFries = _items.Any(i => i.Type == Enums.MenuItemType.Fries);
            var hasDrink = _items.Any(i => i.Type== Enums.MenuItemType.Drink);

            DiscountPercent = 0m;

            if (hasBurger && hasFries && hasDrink)
                DiscountPercent = 0.20m;
            else if (hasBurger && hasDrink)
                DiscountPercent = 0.15m;
            else if (hasBurger && hasFries)
                DiscountPercent = 0.10m;

            Total = Subtotal - (Subtotal * DiscountPercent);
            DiscountAmount = Subtotal - Total;

        }

        public void FinalizeOrder()
        {
            if (!_items.Any(i => i.Type == MenuItemType.Burger))
                throw new BusinessRuleException("Pedido deve conter um sanduíche.");

            CalculateTotals();
        }

        public void ClearItems()
        {
            _items.Clear();
            Subtotal = 0;
            DiscountAmount = 0;
            DiscountPercent = 0;
            Total = 0;
        }
    }
}
