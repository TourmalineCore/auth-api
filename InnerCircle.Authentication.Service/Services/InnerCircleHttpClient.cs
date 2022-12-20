using InnerCircle.Authentication.Service.Services.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace InnerCircle.Authentication.Service.Services
{
    public class InnerCircleHttpClient : IRequestsService
    {
        private readonly HttpClient _client;
        private readonly InnerCircleServiceUrls _urls;

        public InnerCircleHttpClient(IOptions<InnerCircleServiceUrls> urls)
        {
            _client = new HttpClient();
            _urls = urls.Value;
        }

        public async Task SendPasswordCreatingLink(string email, string token)
        {
            var mailSenderLink = $"{_urls.MailServiceUrl}api/mail/send";
            await _client.PostAsJsonAsync(mailSenderLink,
                new { To = email, 
                    Body = $"Go to this link to set a password for your account: {_urls.AuthUIServiceUrl}invitation?code={token}" });
        }

        public async Task<List<string>> GetPrivileges(long accountId)
        {
            var getPrivilegesLink = $"{_urls.AccountsServiceUrl}api/users/findById/{accountId}";
            var response = await _client.GetStringAsync(getPrivilegesLink);
            var privileges = JObject.Parse(response)["privileges"].Values<string>().ToList();
            return privileges;
        }
    }
}
