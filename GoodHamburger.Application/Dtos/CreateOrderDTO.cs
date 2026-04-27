namespace GoodHamburger.Application.Dtos
{
    public class CreateOrderDTO
    {
        public string ClientName { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
