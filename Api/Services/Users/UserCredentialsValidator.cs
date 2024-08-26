using DataAccess.Models;
using DataAccess.Queries;
using Microsoft.AspNetCore.Identity;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Contract;

namespace Api.Services.Users
{
    public class UserCredentialsValidator : IUserCredentialsValidator
    {
        private readonly ILogger<UserCredentialsValidator> _logger;
        private readonly IFindUserQuery _userQuery;
        private readonly UserManager<User> _userManager;

        public UserCredentialsValidator(
            ILogger<UserCredentialsValidator> logger,
            IFindUserQuery userQuery,
            UserManager<User> userManager)
        {
            _logger = logger;
            _userQuery = userQuery;
            _userManager = userManager;
        }

        public async Task<bool> ValidateUserCredentials(string username, string password)
        {
            var user = await _userQuery.FindUserByCorporateEmailAsync(username);

            if(user == null)
            {
                _logger.LogWarning(
                    $"[{nameof(UserCredentialsValidator)}]: User with credentials [{username}] not found.");
                return false;
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if(!isValidPassword)
            {
                _logger.LogWarning(
                    $"[{nameof(UserCredentialsValidator)}]: The password [{password}] is invalid for a user [{username}]");
                return false;
            }

            if(!user.IsBlocked)
                return true;

            _logger.LogWarning(
                $"[{nameof(UserCredentialsValidator)}]: User with credentials [{username}] was blocked.");

            return false;
        }
    }
}