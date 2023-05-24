using System.Security.Claims;
using Data.Queries;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Contract;

namespace InnerCircle.Authentication.Service.Services.Users
{
    public class UserClaimsProvider : IUserClaimsProvider
    {
        private readonly IFindUserQuery _userQuery;
        private readonly ILogger<UserClaimsProvider> _logger;
        private readonly IInnerCircleHttpClient _innerCircleHttpClient;

        public const string PermissionsClaimType = "permissions";

        private const string NameIdentifierClaimType = "nameIdentifier";

        private const string CorporateEmailClaimType = "corporateEmail";

        public UserClaimsProvider(
            IFindUserQuery userQuery,
            ILogger<UserClaimsProvider> logger,
            IInnerCircleHttpClient innerCircleHttpClient)
        {
            _userQuery = userQuery;
            _logger = logger;
            _innerCircleHttpClient = innerCircleHttpClient;
        }

        public async Task<List<Claim>> GetUserClaimsAsync(string login)
        {
            var user = await _userQuery.FindUserByCorporateEmailAsync(login);
            var privileges = await _innerCircleHttpClient.GetPermissions(user.AccountId);

            var claims = new List<Claim>
            {
                new (NameIdentifierClaimType, login),
                new (CorporateEmailClaimType, user.UserName),
                
            };
            privileges.ForEach(x => claims.Add(new Claim(PermissionsClaimType, x.ToString())));

            return claims;
        }
    }
}