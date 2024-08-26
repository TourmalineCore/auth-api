using DataAccess.Models;

namespace DataAccess.Queries
{
    public interface IFindUserQuery
    {
        Task<User?> FindUserByCorporateEmailAsync(string corporateEmail);
    }
}