namespace GoodHamburger.API.Dtos
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemDTO> Items { get; set; }
    }
}
