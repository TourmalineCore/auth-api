using InnerCircle.Authentication.Service.Services.Models;
using InnerCircle.Authentication.Service.Services.Options;
using Microsoft.Extensions.Options;

namespace InnerCircle.Authentication.Service.Services
{
    public class RequestsService : IRequestsService
    {
        private readonly HttpClient _client;
        private readonly InnerCircleServiceUrls _urls;

        public RequestsService(IOptions<InnerCircleServiceUrls> urls)
        {
            _client = new HttpClient();
            _urls = urls.Value;
        }

        public async Task SendPasswordCreatingLink(RegistrationModel model)
        {
            var mailSenderLink = $"{_urls.MailServiceUrl}api/mail/send";
            await _client.PostAsJsonAsync(mailSenderLink,
                new { To = model.PersonalEmail, 
                    Body = $"Go to this link to set a password for your account: {_urls.AuthUIServiceUrl}invitation?code={model.Code}" });
        }
    }
}
