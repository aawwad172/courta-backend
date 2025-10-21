namespace Courta.Domain.Interfaces.Application.Services;

public interface ICurrentUserService
{
    Guid UserId { get; set; }
    Guid TenantId { get; set; }
}
