using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using FluentAssertions;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Tests
{
    public class OrderTests
    {

        [Fact]
        public void Pedido_Completo_Deve_Aplicar_20_Porcento_De_Desconto()
        {
            var order = new Order();

            order.AddItem(new OrderItem(MenuItemType.Burger, "X Burger", 5));
            order.AddItem(new OrderItem(MenuItemType.Fries, "Batata", 2));
            order.AddItem(new OrderItem(MenuItemType.Drink, "Refri", 2.5m));

            order.FinalizeOrder();

            order.Subtotal.Should().Be(9.5m);
            order.DiscountAmount.Should().Be(1.9m);
            order.DiscountPercent.Should().Be(0.20m);
            order.Total.Should().Be(7.6m);
        }

        [Fact]
        public void Adicionar_Pedido_Com_Burger_E_Drink_Deve_Aplicar_Desconto_de_15_por_cento()
        {
            //arrage
            var order = new Order();
            order.AddItem(new OrderItem(MenuItemType.Burger, "X Burger", 5));
            order.AddItem(new OrderItem(MenuItemType.Drink, "Refrigerante", 2.5m));


            //act
            order.FinalizeOrder();

            //assert
            order.Total.Should().Be(6.3750m);
            order.Subtotal.Should().Be(7.50m);
            order.DiscountAmount.Should().Be(1.1250m);
            order.DiscountPercent.Should().Be(0.15m);
        }

        [Fact]
        public void Adicionar_Pedido_Com_Burger_E_Fries_Deve_Aplicar_Desconto_de_10_por_cento()
        {
            //arrage
            var order = new Order();
            order.AddItem(new OrderItem(MenuItemType.Burger, "X Burger", 5));
            order.AddItem(new OrderItem(MenuItemType.Fries, "Batata", 2));

            //act
            order.FinalizeOrder();

            //assert
            order.Total.Should().Be(6.30m);
            order.Subtotal.Should().Be(7.00m);
            order.DiscountAmount.Should().Be(0.70m);
            order.DiscountPercent.Should().Be(0.10m);
        }

        [Fact]
        public void Adicionar_Pedido_Somente_Com_Burger_Nao_Deve_Aplicar_Desconto()
        {
            //arrage
            var order = new Order();
            order.AddItem(new OrderItem(MenuItemType.Burger, "X Burger", 5));

            //act
            order.FinalizeOrder();

            //assert
            order.Total.Should().Be(5.00m);
            order.Subtotal.Should().Be(5.00m);
            order.DiscountAmount.Should().Be(0.00m);
            order.DiscountPercent.Should().Be(0.00m);
        }

        [Fact]
        public void Adicionar_Pedido_Com_Item_Duplicado_Deve_Gerar_Erro()
        {
            //arrage
            var order = new Order();
            order.AddItem(new OrderItem(MenuItemType.Burger, "X Burger", 5));

            //act
            Action act = () => order.AddItem(new OrderItem(MenuItemType.Burger, "X Burger", 5));

            //assert
            act.Should().Throw<BusinessRuleException>();
        }

        [Fact]
        public void Adicionar_Pedido_Sem_Burger_Deve_Gerar_Erro()
        {
            //arrage
            var order = new Order();
            order.AddItem(new OrderItem(MenuItemType.Fries, "Batata", 2));
            order.AddItem(new OrderItem(MenuItemType.Drink, "Refri", 2.5m));

            //act
            Action act = () => order.FinalizeOrder();

            //assert
            act.Should().Throw<BusinessRuleException>();
        }
    }
}
