using SabakaMarket.UserService.Domain.Entities;

namespace SabakaMarket.UserService.Infrastructure.Security;

public interface IJwtProvider
{
    string GenerateToken(User user);
}