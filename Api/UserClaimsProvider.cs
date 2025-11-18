using System.Security.Claims;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Contract;

namespace Api;

public class UserClaimsProvider : IUserClaimsProvider
{
    public const string PermissionClaimType = "permissions";

    public const string CanManageAccounts = "ManageAccounts";

    public const string IsAccountsHardDeleteAllowed = "IsAccountsHardDeleteAllowed";

    public const string AUTO_TESTS_ONLY_IsSetUserPasswordBypassingEmailConfirmationAllowed = "AUTO_TESTS_ONLY_IsSetUserPasswordBypassingEmailConfirmationAllowed";

    public Task<List<Claim>> GetUserClaimsAsync(string login)
    {
        throw new NotImplementedException();
    }
}