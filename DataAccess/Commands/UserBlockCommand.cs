using DataAccess.Queries;

namespace DataAccess.Commands;

public class UserBlockCommand
{
    private readonly AppDbContext _context;
    private readonly GetUserQuery _getUserQuery;

    public UserBlockCommand(AppDbContext context, GetUserQuery getUserQuery)
    {
        _context = context;
        _getUserQuery = getUserQuery;
    }

    public async Task ExecuteAsync(long accountId)
    {
        var user = await _getUserQuery.GetUserByAccountIdAsync(accountId);
        user.IsBlocked = true;
        await _context.SaveChangesAsync();
    }
}