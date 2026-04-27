using GoodHamburger.API.Dtos;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Application.Services.OrderSevice;
using GoodHamburger.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService, IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrder, CancellationToken cancellationToken)
        {
            if (createOrder == null || !createOrder.OrderItems.Any())
                return BadRequest("Pedido deve conter itens.");

            var order = await _orderService.Add(createOrder.OrderItems, cancellationToken);
            order.ClientName = createOrder.ClientName;
            await _unitOfWork.CommitAsync(cancellationToken);

            var orderResponse = new OrderResponseDto
            {
                Id = order.Id,
                ClientName = order.ClientName,
                Items = order.Items.Select(i => new OrderItemDTO
                {
                    Type = i.Type.ToString(),
                    Name = i.Name,
                    Price = i.Price
                }).ToList(),
                Subtotal = order.Subtotal,
                DiscountAmount = order.DiscountAmount,
                DiscountPercent = order.DiscountPercent,
                Total = order.Total
            };
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, orderResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetById(id, cancellationToken);
            if (order == null)
                return NotFound();

            var orderResponse = new OrderResponseDto
            {
                Id = order.Id,
                ClientName = order.ClientName,
                Items = order.Items.Select(i => new OrderItemDTO
                {
                    Type = i.Type.ToString(),
                    Name = i.Name,
                    Price = i.Price
                }).ToList(),
                Subtotal = order.Subtotal,
                DiscountAmount = order.DiscountAmount,
                DiscountPercent = order.DiscountPercent,
                Total = order.Total
            };
            return Ok(orderResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAll(cancellationToken);
            var orderResponses = orders.Select(order => new OrderResponseDto
            {
                Id = order.Id,
                ClientName = order.ClientName,
                Items = order.Items.Select(i => new OrderItemDTO
                {
                    Type = i.Type.ToString(),
                    Name = i.Name,
                    Price = i.Price
                }).ToList(),
                Subtotal = order.Subtotal,
                DiscountAmount = order.DiscountAmount,
                DiscountPercent = order.DiscountPercent,
                Total = order.Total
            }).ToList();
            return Ok(orderResponses);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDTO updateOrderItemDto, CancellationToken cancellationToken)
        {
            if (updateOrderItemDto == null)
                return BadRequest("Dados de atualização inválidos.");

            var updatedOrder = await _orderService.Update(updateOrderItemDto, cancellationToken);
            updatedOrder.ClientName = updateOrderItemDto.ClientName;
            await _unitOfWork.CommitAsync(cancellationToken);

            if (updatedOrder == null)
                return NotFound();

            var orderResponse = new OrderResponseDto
            {
                Id = updatedOrder.Id,
                ClientName = updatedOrder.ClientName,
                Items = updatedOrder.Items.Select(i => new OrderItemDTO
                {
                    Type = i.Type.ToString(),
                    Name = i.Name,
                    Price = i.Price
                }).ToList(),
                Subtotal = updatedOrder.Subtotal,
                DiscountAmount = updatedOrder.DiscountAmount,
                DiscountPercent = updatedOrder.DiscountPercent,
                Total = updatedOrder.Total
            };
            return Ok(orderResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id, CancellationToken cancellationToken)
        {
            await _orderService.Delete(id, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return NoContent();
        }
    }
}