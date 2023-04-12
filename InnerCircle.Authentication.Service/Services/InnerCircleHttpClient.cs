using System.Text.Json;
using System.Web;
using InnerCircle.Authentication.Service.Services.Options;
using Microsoft.Extensions.Options;

namespace InnerCircle.Authentication.Service.Services
{
    public class InnerCircleHttpClient : IInnerCircleHttpClient
    {
        private readonly HttpClient _client;
        private readonly InnerCircleServiceUrls _urls;

        public InnerCircleHttpClient(IOptions<InnerCircleServiceUrls> urls)
        {
            _client = new HttpClient();
            _urls = urls.Value;
        }

        public async Task SendPasswordCreationLink(string corporateEmail, string passwordResetToken)
        {
            var mailSenderLink = $"{_urls.MailServiceUrl}/mail/send-welcome-link";
            await _client.PostAsJsonAsync(mailSenderLink,
                new
                {
                    To = corporateEmail,
                    Subject = "Change your password to Tourmaline Core",
                    Body =
                        $"Go to this link to set a password for your account: {_urls.AuthUIServiceUrl}/change-password?passwordResetToken={HttpUtility.UrlEncode(passwordResetToken)}&corporateEmail={corporateEmail}"
                });
        }

        public async Task SendPasswordResetLink(string corporateEmail, string passwordResetToken)
        {
            var mailSenderLink = $"{_urls.MailServiceUrl}/mail/send-reset-link";
            await _client.PostAsJsonAsync(mailSenderLink,
                new
                {
                    To = corporateEmail,
                    Subject = "Reset your password to Tourmaline Core",
                    Body =
                        $"Go to this link to reset a password for your account: {_urls.AuthUIServiceUrl}/change-password?passwordResetToken={HttpUtility.UrlEncode(passwordResetToken)}&corporateEmail={corporateEmail}"
                });
        }

        public async Task<List<string>> GetPrivileges(long accountId)
        {
            var getPrivilegesLink = $"{_urls.AccountsServiceUrl}/privileges/getByAccountId/{accountId}";
            var response = await _client.GetStringAsync(getPrivilegesLink);
            return JsonSerializer.Deserialize<List<string>>(response);
        }
    }
}