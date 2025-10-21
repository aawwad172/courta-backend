using System.Security.Claims;

using Courta.Domain.Entities;
using Courta.Domain.Entities.Authentication;

namespace Courta.Domain.Interfaces.Application.Services;

public interface IJwtService
{
    Task<string> GenerateAccessTokenAsync(User user);
    RefreshToken CreateRefreshTokenEntity(
        User user,
        Guid tokenFamilyId);
    Task<ClaimsPrincipal> ValidateToken(string token);
}
