using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Domain.Entities
{
    public class OrderItem
    {
        public MenuItemType Type { get; }
        public string? Name { get; }
        public decimal Price { get; }

        private OrderItem() // EF
        {
            
        }

        public OrderItem(MenuItemType type, string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessRuleException("Nome inválido");

            if (price <= 0)
                throw new BusinessRuleException("Preço deve ser maior que zero");

            Type = type;
            Name = name;
            Price = price;
        }
    }
}
