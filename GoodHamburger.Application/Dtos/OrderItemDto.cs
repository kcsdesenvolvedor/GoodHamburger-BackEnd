using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Application.Dtos
{
    public class OrderItemDto
    {
        public MenuItemType Type { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
