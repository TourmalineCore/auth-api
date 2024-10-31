using System.Text.Json;
using System.Web;
using Api.Services.Options;
using Microsoft.Extensions.Options;

namespace Api.Services
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
            var response = await _client.PostAsJsonAsync(mailSenderLink,
                new
                {
                    To = corporateEmail,
                    Subject = "Change your password to Tourmaline Core",
                    Body =
                        $"Go to this link to set a password for your account: {_urls.AuthUIServiceUrl}/change-password?passwordResetToken={HttpUtility.UrlEncode(passwordResetToken)}&corporateEmail={corporateEmail}"
                });

            System.Console.WriteLine("**** SendMail result: " + response.StatusCode);
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

        public async Task<List<string>> GetPermissions(long accountId)
        {
            var accountPermissionsLink = $"{_urls.AccountsServiceUrl}/internal/account-permissions/{accountId}";
            var response = await _client.GetStringAsync(accountPermissionsLink);
            return JsonSerializer.Deserialize<List<string>>(response);
        }

        public async Task<long> GetTenantId(long accountId)
        {
            var link = $"{_urls.AccountsServiceUrl}/internal/get-tenantId-by-accountId/{accountId}";
            var response = await _client.GetStringAsync(link);
            return JsonSerializer.Deserialize<long>(response);
        }
    }
}