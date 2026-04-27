namespace GoodHamburger.Application.Dtos
{
    public class UpdateOrderDTO
    {
        public Guid OrderId { get; set; }
        public string ClientName { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
