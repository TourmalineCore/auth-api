using Data.Models;
using Data.Queries;
using InnerCircle.Authentication.Service.Services.Models;
using Microsoft.AspNetCore.Identity;

namespace InnerCircle.Authentication.Service.Services
{
    public class UsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserQuery _userQuery;
        private readonly IInnerCircleHttpClient _innerCircleHttpClient;
        private readonly ILogger<UsersService> _logger;
        private readonly IPasswordValidator<User> _passwordValidator;

        public UsersService(
            UserManager<User> userManager, 
            IUserQuery userQuery,
            IInnerCircleHttpClient innerCircleHttpClient, 
            ILogger<UsersService> logger, 
            IPasswordValidator<User> passwordValidator)
        {
            _userManager = userManager;
            _userQuery = userQuery;
            _innerCircleHttpClient = innerCircleHttpClient;
            _logger = logger;
            _passwordValidator = passwordValidator;
        }

        public async Task RegisterAsync(RegistrationModel registrationModel)
        {
            var user = await _userQuery.FindUserByCorporateEmailAsync(registrationModel.CorporateEmail);

            if (user != null)
            {
                throw new NullReferenceException($"User with the corporate email [{registrationModel.CorporateEmail}] already exists");
            }

            var newUser = new User
            {
                UserName = registrationModel.CorporateEmail, 
                AccountId = registrationModel.AccountId
            };

            var userPassword = PasswordGenerator.GeneratePassword(5, 5, 5, 5);
            await _userManager.CreateAsync(newUser, userPassword);

            try
            {
                var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(newUser);
                await _innerCircleHttpClient.SendPasswordCreationLink(registrationModel.CorporateEmail, passwordResetToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"[{nameof(UsersService)}]: Couldn't send a link on password creation for user [{registrationModel.CorporateEmail}]. Exception details: {ex.Message}");
            }
        }

        public async Task ResetPasswordAsync(string corporateEmail)
        {
            var user = await _userQuery.FindUserByCorporateEmailAsync(corporateEmail);
            if (user == null) throw new NullReferenceException("User doesn't exists");
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _innerCircleHttpClient.SendPasswordResetLink(corporateEmail, resetToken);
        }

        public async Task ChangePasswordAsync(PasswordChangeModel passwordChangeModel)
        {
            var user = await _userQuery.FindUserByCorporateEmailAsync(passwordChangeModel.CorporateEmail);

            if (user == null)
            {
                throw new NullReferenceException($"User with the corporate email [{passwordChangeModel.CorporateEmail}] doesn't exists");
            }

            var passwordResetTokenIsValid = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider,
                UserManager<User>.ResetPasswordTokenPurpose, passwordChangeModel.PasswordResetToken);

            if (!passwordResetTokenIsValid)
            {
                throw new Exception("Password reset token is invalid");
            }

            var newPasswordValidationResult = await _passwordValidator.ValidateAsync(_userManager, user, passwordChangeModel.NewPassword);

            if (!newPasswordValidationResult.Succeeded)
            {
                throw new ArgumentException("New password is invalid");
            }

            await _userManager.ResetPasswordAsync(user, passwordChangeModel.PasswordResetToken,
                passwordChangeModel.NewPassword);
        }
    }
}