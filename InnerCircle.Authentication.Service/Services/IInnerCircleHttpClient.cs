using Newtonsoft.Json.Linq;

namespace InnerCircle.Authentication.Service.Services
{
    public interface IInnerCircleHttpClient
    {
        Task SendPasswordCreatingLink(string email, string token);
        Task<List<string>> GetPrivileges(long accountId);
    }
}
