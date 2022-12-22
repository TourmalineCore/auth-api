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

        public UsersService(UserManager<User> userManager, IUserQuery userQuery, IInnerCircleHttpClient innerCircleHttpClient)
        {
            _userManager = userManager;
            _userQuery = userQuery;
            _innerCircleHttpClient = innerCircleHttpClient;
        }

        public async Task RegisterAsync(RegistrationModel requestModel)
        {
            var account = new User { UserName = requestModel.Login, AccountId = requestModel.AccountId };
            await _userManager.CreateAsync(account, 
                GeneratePassword(5,5,5,5));

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(account);
            await _innerCircleHttpClient.SendPasswordCreatingLink(requestModel.PersonalEmail, resetToken);
        }

        public async Task ResetPasswordAsync(string corporateEmail)
        {
            var account = await _userQuery.FindUserByUserNameAsync(corporateEmail);
            if (account == null) throw new Exception("User doesn't exists");
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(account);
            await _innerCircleHttpClient.SendPasswordResetLink(corporateEmail, resetToken);
        }

        public async Task CreatePasswordAsync(CreatePasswordModel requestModel)
        {
            var account = await _userQuery.FindUserByUserNameAsync(requestModel.Login);
            await _userManager.ResetPasswordAsync(account, requestModel.UserResetPasswordToken, requestModel.Password);
        }

        private string GeneratePassword(int lowercase, int uppercase, int numerics, int symbols)
        {
            string lowers = "abcdefghijklmnopqrstuvwxyz";
            string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string number = "0123456789";
            string symbol = "!@#$%^&*()_-+=";

            Random random = new Random();

            string generated = "!";
            for (int i = 1; i <= lowercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString()
                );

            for (int i = 1; i <= uppercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString()
                );

            for (int i = 1; i <= numerics; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString()
                );

            for (int i = 1; i <= symbols; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    symbol[random.Next(symbol.Length - 1)].ToString()
                );

            return generated.Replace("!", string.Empty);
        }
    }
}
