using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SabakaMarket.PaymentService.Application.DTOs;
using SabakaMarket.PaymentService.Application.Services;
using System.Security.Claims;

namespace SabakaMarket.PaymentService.API.Controllers;

[Authorize]
[ApiController]
[Route("api/payments")]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    [HttpPost("init")]
    public async Task<ActionResult<PaymentTransactionDto>> Initialize([FromBody] ProcessPaymentDto dto)
    {
        var buyerId = GetCurrentUserId();
        if (buyerId == Guid.Empty) return Unauthorized();

        var result = await paymentService.InitializePaymentAsync(buyerId, dto);
        return Ok(result);
    }

    [HttpPost("{orderId:guid}/complete")]
    public async Task<IActionResult> Complete(Guid orderId)
    {
        var success = await paymentService.CompletePaymentAsync(orderId);
        return success ? Ok(new { Message = "Платеж успешно проведен." }) : BadRequest();
    }

    [HttpGet("my")]
    public async Task<ActionResult<IReadOnlyList<PaymentTransactionDto>>> GetMyTransactions()
    {
        var buyerId = GetCurrentUserId();
        var transactions = await paymentService.GetMyTransactionsAsync(buyerId);
        return Ok(transactions);
    }

    private Guid GetCurrentUserId()
    {
        var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                         ?? User.FindFirst("sub")?.Value;
        return Guid.TryParse(claimValue, out var userId) ? userId : Guid.Empty;
    }
}