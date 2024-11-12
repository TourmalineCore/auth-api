using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Tests.Integration;

public class LogoutTests : TestBase
{
    public LogoutTests(WebApplicationFactory<Program> factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task LogoutWithValidToken_Return200()
    {
        var loginResult = await LoginAsync("admin", "admin");

        var logoutStatusCode = await LogoutAsync(loginResult.authModel);

        Assert.Equal(HttpStatusCode.OK, logoutStatusCode);
    }
}