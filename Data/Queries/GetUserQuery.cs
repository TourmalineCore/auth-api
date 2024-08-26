using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Queries;

public class GetUserQuery
{
    private readonly AppDbContext _context;

    public GetUserQuery(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByAccountIdAsync(long accountId)
    {
        var user = await _context
            .Users
            .SingleOrDefaultAsync(x => x.AccountId == accountId);

        if (user == null)
        {
            throw new NullReferenceException("User not found");
        }

        return user;
    }
}