using Data.Models;

namespace Data.Queries
{
    public interface IUserQuery
    {
        Task<User?> FindUserByUserNameAsync(string login);
    }
}