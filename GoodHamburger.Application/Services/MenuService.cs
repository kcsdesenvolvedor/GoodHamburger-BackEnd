using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Application.Services
{
    public static class MenuService
    {
        public static List<OrderItem> Items => new()
        {
            new(MenuItemType.Burger, "X Burger", 5.00m),
            new(MenuItemType.Burger, "X Egg", 4.50m),
            new(MenuItemType.Burger, "X Bacon", 7.00m),
            new(MenuItemType.Fries, "Batata frita", 2.00m),
            new(MenuItemType.Drink, "Refrigerante", 2.50m)
        };
    }
}
