using Courta.Domain.Entities.Authentication;
using Courta.Domain.Interfaces.Infrastructure.IRepositories;

using Microsoft.EntityFrameworkCore;

namespace Courta.Infrastructure.Persistence.Repositories;

public class RoleRepository(ApplicationDbContext context) : Repository<Role>(context), IRoleRepository
{
    public async Task<Role?> GetRoleByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
    }
}
