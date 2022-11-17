using Data;
using Data.Queries;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Contract;

namespace InnerCircle.Authentication.Service.Services.Users
{
    public class UserCredentialsValidator : IUserCredentialsValidator
    {
        private readonly ILogger<UserCredentialsValidator> _logger;
        private readonly IUserQuery _userQuery;

        public UserCredentialsValidator(
            ILogger<UserCredentialsValidator> logger,
            IUserQuery userQuery)
        {
            _logger = logger;
            _userQuery = userQuery;
        }

        public async Task<bool> ValidateUserCredentials(string username, string password)
        {
            var user = await _userQuery.FindUserByUserNameAsync(username);

            if (user == null)
            {
                _logger.LogWarning($"[{nameof(UserCredentialsValidator)}]: User with credentials [{username}] not found.");
                return false;
            }

            if (user.IsBlocked)
            {
                _logger.LogWarning(
                    $"[{nameof(UserCredentialsValidator)}]: User with credentials [{username}] was blocked.");
                return false;
            }
            
            return true;
        }
    }
}