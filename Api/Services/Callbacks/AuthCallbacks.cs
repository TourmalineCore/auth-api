using TourmalineCore.AspNetCore.JwtAuthentication.Core.Middlewares.Login.Models;

namespace Api.Services.Callbacks;

internal class AuthCallbacks
{
    public Task OnLoginExecuting(LoginModel data)
    {
        //Make something
        return Task.CompletedTask;
    }
    public Task OnLoginExecuted(LoginModel data)
    {
        //Make something
        return Task.CompletedTask;
    }
}