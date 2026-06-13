using SabakaMarket.UserService.Application.DTOs;

namespace SabakaMarket.UserService.Application.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterUserDto dto);
    Task<AuthResponseDto> LoginAsync(LoginUserDto dto);
}