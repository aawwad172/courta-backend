using Courta.Domain.Entities.Authentication;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courta.Infrastructure.Configurations.Seed;

public class RolesPermissionsSeed : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        // -----------------------------------------------------------------------------------
        // Define Permission GUID sets for each role
        // -----------------------------------------------------------------------------------

        // 1. All Guids (For Super Admin, who gets EVERYTHING)
        Guid[] allPermissionGuids = PermissionSeedHelper.AllPermissionMappings.Select(m => m.Id).ToArray();

        // 2. Tenant Admin Guids (Full management access within the tenant)
        Guid[] tenantAdminGuids =
        [
            PermissionGuids.UserSelfRead,
            PermissionGuids.UserSelfUpdate,
            PermissionGuids.TenantUserRead,
            PermissionGuids.TenantUserManage,
            PermissionGuids.CourtRead,
            PermissionGuids.CourtCreate,
            PermissionGuids.CourtUpdate,
            PermissionGuids.CourtDelete,
            PermissionGuids.TimeRead,
            PermissionGuids.TimeCreate,
            PermissionGuids.TimeUpdate,
            PermissionGuids.TimeDelete,
            PermissionGuids.TokenRefresh // Critical for API access
        ];

        // 3. Basic User Guids (Read-only, self-management, refresh token)
        Guid[] basicUserGuids =
        [
            PermissionGuids.UserSelfRead,
            PermissionGuids.UserSelfUpdate,
            PermissionGuids.TimeRead,
            PermissionGuids.TokenRefresh // Critical for API access
        ];

        // -----------------------------------------------------------------------------------
        // Apply HasData using the defined GUID sets
        // -----------------------------------------------------------------------------------

        var seedData = new List<RolePermission>();

        // 1. SUPER ADMIN (Links to ALL permissions)
        seedData.AddRange(LinkPermissions(AuthSeedConstants.RoleIdSuperAdmin, allPermissionGuids));

        // 2. TENANT ADMIN (Links to tenant management permissions)
        // Assuming AuthSeedConstants.RoleIdTenantAdmin replaces the old AdminId
        seedData.AddRange(LinkPermissions(AuthSeedConstants.RoleIdAdmin, tenantAdminGuids));

        // 3. BASIC USER (Links to minimal permissions)
        // Assuming AuthSeedConstants.RoleIdBasicUser replaces the old UserId
        seedData.AddRange(LinkPermissions(AuthSeedConstants.RoleIdUser, basicUserGuids));

        builder.HasData(seedData);
    }

    // Helper to link a role ID to a list of permission GUIDs
    private static IEnumerable<RolePermission> LinkPermissions(Guid roleId, IEnumerable<Guid> permissionGuids)
    {
        return permissionGuids.Select(permissionId => new RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId
        });
    }
}
