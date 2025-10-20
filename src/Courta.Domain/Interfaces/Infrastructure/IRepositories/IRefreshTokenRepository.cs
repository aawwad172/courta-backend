using Courta.Domain.Entities.Authentication;

namespace Courta.Domain.Interfaces.Infrastructure.IRepositories;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenAsync(string token, Guid userId);
}
