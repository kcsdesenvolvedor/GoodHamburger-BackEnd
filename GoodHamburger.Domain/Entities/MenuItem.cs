using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities
{
    public class MenuItem
    {
        public string Name { get; set; }
        public MenuItemType Type { get; set; }
        public decimal Price { get; set; }
    }
}
