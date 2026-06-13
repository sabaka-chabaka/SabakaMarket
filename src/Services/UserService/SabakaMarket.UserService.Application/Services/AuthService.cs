using SabakaMarket.UserService.Application.DTOs;
using SabakaMarket.UserService.Domain.Entities;
using SabakaMarket.UserService.Domain.Repositories;
using SabakaMarket.UserService.Infrastructure.Security;

namespace SabakaMarket.UserService.Application.Services;

public class AuthService(
    IUserRepository users,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider) : IAuthService
{
    public async Task<AuthResponseDto> RegisterAsync(RegisterUserDto dto)
    {
        if (await users.ExistsByEmailAsync(dto.Email)) throw new ArgumentException("Email already exists");
        var hashPassword = passwordHasher.HashPassword(dto.Password);
        
        var user = new User(Guid.NewGuid(), dto.Email, hashPassword, dto.Username);
        
        await users.AddAsync(user);
        
        return new AuthResponseDto(user.Id, user.Username, user.Email, jwtProvider.GenerateToken(user));
    }

    public async Task<AuthResponseDto> LoginAsync(LoginUserDto dto)
    {
        var user = await users.GetByEmailAsync(dto.Email);
        if (user == null) throw new UnauthorizedAccessException("Incorrect email or password");
        if (!passwordHasher.VerifyPassword(dto.Password, user.PasswordHash)) throw new UnauthorizedAccessException("Incorrect email or password");
        if (user.IsBlocked) throw new UnauthorizedAccessException("User is blocked");
        var token = jwtProvider.GenerateToken(user);

        return new AuthResponseDto(user.Id, user.Username, user.Email, token);
    }
}