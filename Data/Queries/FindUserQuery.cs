using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Queries
{
    public class FindUserQuery : IFindUserQuery
    {
        private readonly AppDbContext _context;

        public FindUserQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> FindUserByCorporateEmailAsync(string corporateEmail)
        {
            return await GetUsersAsQueryable()
                .SingleOrDefaultAsync(x => x.UserName == corporateEmail);
        }

        private IQueryable<User?> GetUsersAsQueryable()
        {
            return _context
                .Set<User>()
                .AsQueryable();
        }
    }
}