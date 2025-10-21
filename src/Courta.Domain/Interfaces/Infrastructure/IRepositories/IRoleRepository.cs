using Courta.Domain.Entities.Authentication;

namespace Courta.Domain.Interfaces.Infrastructure.IRepositories;

public interface IRoleRepository
{
    Task<Role?> GetRoleByNameAsync(string name);
}
