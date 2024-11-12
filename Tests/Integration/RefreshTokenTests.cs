using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Models;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Models.Response;
using Xunit;

namespace Tests.Integration;

public class RefreshTokenTests : TestBase
{
    public RefreshTokenTests(WebApplicationFactory<Program> factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Login_ThenRefreshToken_ReturnsNewTokens()
    {
        var (_, authModel) = await LoginAsync("admin", "admin");

        var (_, authModelWithNewTokens) = await CallRefreshAsync(authModel);

        Assert.False(string.IsNullOrWhiteSpace(authModelWithNewTokens.AccessToken.Value));
        Assert.False(string.IsNullOrWhiteSpace(authModelWithNewTokens.RefreshToken.Value));
    }

    [Fact]
    public async Task RefreshWithInvalidToken_Returns401()
    {
        var invalidAuthResponseModel = new AuthResponseModel
        {
            RefreshToken = new TokenModel
            {
                Value = Guid.NewGuid().ToString()
            },
            AccessToken = new TokenModel
            {
                Value = string.Empty
            }
        };

        var (response, _) = await CallRefreshAsync(invalidAuthResponseModel);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
}