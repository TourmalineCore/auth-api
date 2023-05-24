using Data.Queries;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Contract;

namespace InnerCircle.Authentication.Service.Services.Users
{
    public class UserCredentialsValidator : IUserCredentialsValidator
    {
        private readonly ILogger<UserCredentialsValidator> _logger;
        private readonly IFindUserQuery _userQuery;

        public UserCredentialsValidator(
            ILogger<UserCredentialsValidator> logger,
            IFindUserQuery userQuery)
        {
            _logger = logger;
            _userQuery = userQuery;
        }

        public async Task<bool> ValidateUserCredentials(string username, string password)
        {
            var user = await _userQuery.FindUserByCorporateEmailAsync(username);

            if (user == null)
            {
                _logger.LogWarning(
                    $"[{nameof(UserCredentialsValidator)}]: User with credentials [{username}] not found.");
                return false;
            }

            if (!user.IsBlocked) return true;

            _logger.LogWarning(
                $"[{nameof(UserCredentialsValidator)}]: User with credentials [{username}] was blocked.");

            return false;
        }
    }
}