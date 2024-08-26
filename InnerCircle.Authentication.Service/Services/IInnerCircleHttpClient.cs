using Newtonsoft.Json.Linq;

namespace InnerCircle.Authentication.Service.Services
{
    public interface IInnerCircleHttpClient
    {
        Task SendPasswordCreationLink(string corporateEmail, string passwordResetToken);
        Task SendPasswordResetLink(string email, string token);
        Task<List<string>> GetPermissions(long accountId);
        Task<long> GetTenantId(long accountId);
    }
}
