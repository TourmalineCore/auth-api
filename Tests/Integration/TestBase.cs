using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Models.Request;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Models.Response;
using Xunit;

namespace Tests.Integration;

public class TestBase : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly JsonSerializerOptions _jsonSerializerSettings;
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    private const string LoginUrl = "/auth/login";
    private const string RegisterUrl = "/auth/register";
    private const string RefreshUrl = "/auth/refresh";
    private const string LogoutUrl = "/auth/logout";

    private readonly string _dbName = Guid.NewGuid().ToString();


    protected TestBase(WebApplicationFactory<Program> factory)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Tests");

        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase(_dbName);
                });
            });
        });

        _client = _factory.CreateClient();

        _jsonSerializerSettings = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            AllowTrailingCommas = true,
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
    }

    internal async Task<(HttpResponseMessage response, AuthResponseModel authModel)> RegistrationAsync(string login,
        string password)
    {
        var body = JsonContent.Create(new RegistrationRequestModel
        {
            Login = login,
            Password = password
        }
        );

        var response = await _client.PostAsync(RegisterUrl, body);
        var result =
            JsonSerializer.Deserialize<AuthResponseModel>(await response.Content.ReadAsStringAsync(),
                _jsonSerializerSettings);
        return (response, result);
    }

    internal async Task<(HttpResponseMessage response, AuthResponseModel authModel)> LoginAsync(string login,
        string password)
    {
        var body = JsonContent.Create(new LoginRequestModel
        {
            Login = login,
            Password = password
        }
        );

        var response = await _client.PostAsync(LoginUrl, body);

        var authModel =
            JsonSerializer.Deserialize<AuthResponseModel>(await response.Content.ReadAsStringAsync(),
                _jsonSerializerSettings);

        return (response, authModel);
    }

    internal async Task<(HttpResponseMessage response, AuthResponseModel authModel)> CallRefreshAsync(
        AuthResponseModel authResponseModel)
    {
        var body = JsonContent.Create(new RefreshTokenRequestModel
        {
            RefreshTokenValue = Guid.Parse(authResponseModel.RefreshToken.Value)
        }
        );

        var response = await _client.PostAsync(RefreshUrl, body);
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", authResponseModel.AccessToken.Value);
        var result =
            JsonSerializer.Deserialize<AuthResponseModel>(await response.Content.ReadAsStringAsync(),
                _jsonSerializerSettings);
        return (response, result);
    }

    internal async Task<HttpStatusCode> LogoutAsync(AuthResponseModel authResponseModel)
    {
        var body = JsonContent.Create(new RefreshTokenRequestModel
        {
            RefreshTokenValue = Guid.Parse(authResponseModel.RefreshToken.Value)
        }
        );

        var response = await _client.PostAsync(LogoutUrl, body);
        return response.StatusCode;
    }
}