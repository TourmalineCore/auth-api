using Data.Models;

namespace Data.Queries
{
    public interface IFindUserQuery
    {
        Task<User?> FindUserByCorporateEmailAsync(string corporateEmail);
    }
}