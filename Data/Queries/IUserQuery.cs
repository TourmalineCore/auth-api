using Data.Models;

namespace Data.Queries
{
    public interface IUserQuery
    {
        Task<User?> FindUserByCorporateEmailAsync(string corporateEmail);
    }
}