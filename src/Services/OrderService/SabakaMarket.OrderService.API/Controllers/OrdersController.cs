using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SabakaMarket.OrderService.Application.DTOs;
using SabakaMarket.OrderService.Application.Services;
using System.Security.Claims;

namespace SabakaMarket.OrderService.API.Controllers;

[Authorize]
[ApiController]
[Route("api/orders")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto)
    {
        var buyerId = GetCurrentUserId();
        if (buyerId == Guid.Empty) return Unauthorized(new { Message = "Не удалось определить пользователя." });

        try
        {
            var result = await orderService.CreateOrderAsync(buyerId, dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> GetById(Guid id)
    {
        var order = await orderService.GetOrderByIdAsync(id);
        if (order is null) return NotFound(new { Message = $"Заказ с ID {id} не найден." });

        var currentUserId = GetCurrentUserId();
        if (order.BuyerId != currentUserId && order.SellerId != currentUserId)
        {
            return Forbid();
        }

        return Ok(order);
    }

    [HttpGet("buyer")]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetMyPurchases()
    {
        var buyerId = GetCurrentUserId();
        var orders = await orderService.GetOrdersByBuyerAsync(buyerId);
        return Ok(orders);
    }

    [HttpGet("seller")]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetMySales()
    {
        var sellerId = GetCurrentUserId();
        var orders = await orderService.GetOrdersBySellerAsync(sellerId);
        return Ok(orders);
    }

    [HttpPost("{id:guid}/pay")]
    public async Task<IActionResult> Pay(Guid id)
    {
        var currentUserId = GetCurrentUserId();
        var order = await orderService.GetOrderByIdAsync(id);
        
        if (order is null) return NotFound();
        if (order.BuyerId != currentUserId) return Forbid();

        var success = await orderService.PayOrderAsync(id);
        return success ? Ok(new { Message = "Заказ успешно оплачен." }) : BadRequest();
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var currentUserId = GetCurrentUserId();
        var order = await orderService.GetOrderByIdAsync(id);
        
        if (order is null) return NotFound();
        if (order.BuyerId != currentUserId && order.SellerId != currentUserId) return Forbid(); 

        var success = await orderService.CancelOrderAsync(id);
        return success ? Ok(new { Message = "Заказ отменен." }) : BadRequest();
    }

    private Guid GetCurrentUserId()
    {
        var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                         ?? User.FindFirst("sub")?.Value;

        return Guid.TryParse(claimValue, out var userId) ? userId : Guid.Empty;
    }
}