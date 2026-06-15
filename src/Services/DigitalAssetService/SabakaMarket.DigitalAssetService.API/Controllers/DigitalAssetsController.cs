using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SabakaMarket.DigitalAssetService.Application.DTOs;
using SabakaMarket.DigitalAssetService.Application.Services;
using System.Security.Claims;

namespace SabakaMarket.DigitalAssetService.API.Controllers;

[Authorize]
[ApiController]
[Route("api/assets")]
public class DigitalAssetsController(IDigitalAssetService assetService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<DigitalAssetDto>> Upload([FromBody] UploadAssetDto dto)
    {
        var sellerId = GetCurrentUserId();
        if (sellerId == Guid.Empty) return Unauthorized(new { Message = "Cannot get the seller id from the token." });

        try
        {
            var result = await assetService.UploadAssetAsync(sellerId, dto);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet("my")]
    public async Task<ActionResult<IReadOnlyList<DigitalAssetDto>>> GetMyAssets()
    {
        var sellerId = GetCurrentUserId();
        var assets = await assetService.GetMyAssetsAsync(sellerId);
        return Ok(assets);
    }

    private Guid GetCurrentUserId()
    {
        var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                         ?? User.FindFirst("sub")?.Value;

        return Guid.TryParse(claimValue, out var userId) ? userId : Guid.Empty;
    }
}