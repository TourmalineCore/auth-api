using System.Security.Claims;
using Data.Queries;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Contract;

namespace InnerCircle.Authentication.Service.Services.Users
{
    public class UserClaimsProvider : IUserClaimsProvider
    {
        private readonly IUserQuery _userQuery;
        private readonly ILogger<UserClaimsProvider> _logger;
        private readonly IRequestsService _requestsService;

        public const string PermissionsClaimType = "permissions";

        private const string NameIdentifireClaimType = "nameIdentifier";

        private const string IdClaimType = "id";

        public UserClaimsProvider(
            IUserQuery userQuery,
            ILogger<UserClaimsProvider> logger,
            IRequestsService requestsService)
        {
            _userQuery = userQuery;
            _logger = logger;
            _requestsService = requestsService;
        }

        public async Task<List<Claim>> GetUserClaimsAsync(string login)
        {
            var user = await _userQuery.FindUserByUserNameAsync(login);
            var privileges = await _requestsService.GetPrivileges(user.AccountId);

            var claims = new List<Claim>
            {
                new Claim(NameIdentifireClaimType, login),
                new Claim(IdClaimType, user.AccountId.ToString()),
                
            };
            privileges.ForEach(x => claims.Add(new Claim(PermissionsClaimType, x.ToString())));

            return claims;
        }
    }
}