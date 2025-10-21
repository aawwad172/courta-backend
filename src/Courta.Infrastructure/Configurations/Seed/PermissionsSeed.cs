using Courta.Domain.Entities.Authentication;
using Courta.Domain.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courta.Infrastructure.Configurations.Seed;

public class PermissionsSeed : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        // This array now contains 14 stable entities ready for seeding
        builder.HasData(GetSeedPermissions());
    }

    private static IEnumerable<Permission> GetSeedPermissions()
    {
        return PermissionSeedHelper.AllPermissionMappings.Select(mapping =>
            new Permission
            {
                // Uses the stable GUID from PermissionGuids
                Id = mapping.Id,
                // Uses the stable string constant from PermissionConstants
                Name = mapping.Name,
                Description = $"Grants permission to perform the action: {mapping.Name}",
                CreatedAt = AuthSeedConstants.SeedDateUtc,
                CreatedBy = AuthSeedConstants.SystemUserId
            });
    }
}

// 1. GUIDs: Holds all the stable, unique GUID IDs for permissions
public static class PermissionGuids
{
    // --- User & Identity Guids ---
    public static readonly Guid UserSelfRead = new("019a0127-4b64-7881-b0f2-6133c66068bf"); // User.Read.Self
    public static readonly Guid UserSelfUpdate = new("019a0127-4b64-7ce7-93bd-66f11d5956f4"); // User.Update.Self
    public static readonly Guid TenantUserRead = new("019a0127-4b64-7479-b135-2a9e7f58538c"); // Tenant.User.Read
    public static readonly Guid TenantUserManage = new("019a0127-4b64-79e8-8e16-390b93181a93"); // Tenant.User.Manage
    public static readonly Guid ConfigManage = new("019a0127-4b64-72f6-90de-cd3027dbca1e"); // Config.Manage
    public static readonly Guid TokenRefresh = new("019a0127-4b64-72fe-bedb-dbc0f3fa7079"); // Token.Refresh

    // --- Court Management Guids ---
    public static readonly Guid CourtRead = new("019a0127-4b64-786a-a608-dafdb01715bc"); // Court.Read
    public static readonly Guid CourtCreate = new("019a0127-4b64-7f37-8456-bde593549ad1"); // Court.Create
    public static readonly Guid CourtUpdate = new("019a0127-4b64-7517-b4e8-2976364ef64c"); // Court.Update
    public static readonly Guid CourtDelete = new("019a0127-4b64-7fc2-a465-2605da389765"); // Court.Delete

    // --- Available Time Management Guids ---
    public static readonly Guid TimeRead = new("019a0127-4b64-7d4c-8f93-2d35d99d7847"); // Time.Read
    public static readonly Guid TimeCreate = new("019a0127-4b64-7917-97c1-bb501756efc6"); // Time.Create
    public static readonly Guid TimeUpdate = new("019a0127-4b64-77f1-a585-e00411ab0724"); // Time.Update
    public static readonly Guid TimeDelete = new("019a0127-4b64-7f3e-b84b-62f0d9e08975"); // Time.Delete
}

// 2. Mapping Structure (To be used by the seed configuration)
public record struct PermissionMapping(Guid Id, string Name);
public static class PermissionSeedHelper
{
    // Note: The string constants must be defined in PermissionConstants for this to compile.
    public static readonly PermissionMapping[] AllPermissionMappings =
    [
        new PermissionMapping(PermissionGuids.UserSelfRead, PermissionConstants.UserSelfRead),
        new PermissionMapping(PermissionGuids.UserSelfUpdate, PermissionConstants.UserSelfUpdate),
        new PermissionMapping(PermissionGuids.TenantUserRead, PermissionConstants.TenantUserRead),
        new PermissionMapping(PermissionGuids.TenantUserManage, PermissionConstants.TenantUserManage),
        new PermissionMapping(PermissionGuids.ConfigManage, PermissionConstants.ConfigManage),
        new PermissionMapping(PermissionGuids.TokenRefresh, PermissionConstants.TokenRefresh),
        new PermissionMapping(PermissionGuids.CourtRead, PermissionConstants.CourtRead),
        new PermissionMapping(PermissionGuids.CourtCreate, PermissionConstants.CourtCreate),
        new PermissionMapping(PermissionGuids.CourtUpdate, PermissionConstants.CourtUpdate),
        new PermissionMapping(PermissionGuids.CourtDelete, PermissionConstants.CourtDelete),
        new PermissionMapping(PermissionGuids.TimeRead, PermissionConstants.TimeRead),
        new PermissionMapping(PermissionGuids.TimeCreate, PermissionConstants.TimeCreate),
        new PermissionMapping(PermissionGuids.TimeUpdate, PermissionConstants.TimeUpdate),
        new PermissionMapping(PermissionGuids.TimeDelete, PermissionConstants.TimeDelete)
    ];
}
