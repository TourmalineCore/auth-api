using Newtonsoft.Json.Linq;

namespace InnerCircle.Authentication.Service.Services
{
    public interface IRequestsService
    {
        Task SendPasswordCreatingLink(string email, string token);
        Task<List<string>> GetPrivileges(long accountId);
    }
}
