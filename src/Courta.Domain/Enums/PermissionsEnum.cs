namespace Courta.Domain.Enums;

public static class PermissionConstants
{
    // User & Identity Permissions
    public const string UserSelfRead = "User.Read.Self";
    public const string UserSelfUpdate = "User.Update.Self";
    public const string TenantUserRead = "Tenant.User.Read";
    public const string TenantUserManage = "Tenant.User.Manage";
    public const string ConfigManage = "Config.Manage";
    public const string TokenRefresh = "Token.Refresh"; // Critical for all users

    // Court Management Permissions
    public const string CourtRead = "Court.Read";
    public const string CourtCreate = "Court.Create";
    public const string CourtUpdate = "Court.Update";
    public const string CourtDelete = "Court.Delete";

    // Available Time Management Permissions
    public const string TimeRead = "Time.Read";
    public const string TimeCreate = "Time.Create";
    public const string TimeUpdate = "Time.Update";
    public const string TimeDelete = "Time.Delete";

    // Array containing all Tenant Admin Permissions for easy assignment
    public static readonly string[] TenantAdminPermissions =
    [
        UserSelfRead, UserSelfUpdate,
        TenantUserRead, TenantUserManage,
        CourtRead, CourtCreate, CourtUpdate, CourtDelete,
        TimeRead, TimeCreate, TimeUpdate, TimeDelete,
        TokenRefresh // Must include the refresh permission
    ];

    // Array containing all Basic User Permissions
    public static readonly string[] BasicUserPermissions =
    [
        UserSelfRead, TimeRead, TokenRefresh
    ];

}
