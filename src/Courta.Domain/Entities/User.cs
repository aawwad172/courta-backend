
using Courta.Domain.Entities.Authentication;
using Courta.Domain.Interfaces.Domain.Auditing;
namespace Courta.Domain.Entities;


public class User : IBaseEntity
{
    public required Guid Id { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required DateTime CreatedAt { get; init; }
    public required Guid CreatedBy { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public required bool IsActive { get; set; }
    public required string SecurityStamp { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public required bool IsDeleted { get; set; } = false;
    public required bool IsVerified { get; set; } = false;
    
    // --- TENANCY FIELDS ---
    // 1. Foreign Key (Nullable is REQUIRED for Super Admin)
    public Guid? TenantId { get; set; }
}
