using InnerCircle.Authentication.Service.Services.Models;

namespace InnerCircle.Authentication.Service.Services
{
    public interface IRequestsService
    {
        Task SendPasswordCreatingLink(RegistrationModel model);
    }
}
