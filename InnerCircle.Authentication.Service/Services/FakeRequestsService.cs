using InnerCircle.Authentication.Service.Services.Models;

namespace InnerCircle.Authentication.Service.Services
{
    public class FakeRequestsService : IRequestsService
    {
        public Task SendPasswordCreatingLink(RegistrationModel model)
        {
            return Task.CompletedTask;
        }
    }
}
