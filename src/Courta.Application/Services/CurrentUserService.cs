using Courta.Domain.Interfaces.Application.Services;

namespace Courta.Application.Services;

public class CurrentUserService : ICurrentUserService
{
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
}
