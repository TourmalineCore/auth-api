namespace InnerCircle.Authentication.Service.Services
{
    public interface IRequestsService
    {
        Task SendPasswordCreatingLink(string email, string token);
    }
}
