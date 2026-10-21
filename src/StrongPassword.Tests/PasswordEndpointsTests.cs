using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using StrongPassword.Api.Endpoints;

namespace StrongPassword.Tests;

public sealed class PasswordEndpointsTests
{
    [Fact]
    public async Task PostPassword_ReturnsCreatedPasswordAndGuidThatCanBeRetrieved()
    {
        using var factory = new PasswordApiFactory();
        using var client = factory.CreateClient();

        var createResponse = await client.PostAsync("/api/v1/passwords", content: null);
        var createdPassword = await createResponse.Content.ReadFromJsonAsync<PasswordResponse>();

        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        Assert.NotNull(createdPassword);
        Assert.NotEqual(Guid.Empty, createdPassword.Guid);
        Assert.True(createdPassword.Password.Length >= 16);
        Assert.Contains(createdPassword.Password, character =>
            char.IsPunctuation(character) || char.IsSymbol(character));
        Assert.DoesNotContain(createdPassword.Password, char.IsWhiteSpace);

        var getResponse = await client.GetAsync($"/api/v1/passwords/{createdPassword.Guid}");
        var retrievedPassword = await getResponse.Content.ReadFromJsonAsync<PasswordResponse>();

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal(createdPassword, retrievedPassword);
    }

    [Fact]
    public async Task GetPassword_ReturnsBadRequestProblemDetailsForMalformedGuid()
    {
        using var factory = new PasswordApiFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/v1/passwords/not-a-guid");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetPassword_ReturnsNotFoundForUnknownGuid()
    {
        using var factory = new PasswordApiFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync($"/api/v1/passwords/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private sealed class PasswordApiFactory : WebApplicationFactory<Program>
    {
        private readonly string _databasePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.db");

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, configuration) =>
            {
                configuration.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["ConnectionStrings:DefaultConnection"] = $"Data Source={_databasePath};Pooling=False"
                });
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing && File.Exists(_databasePath))
            {
                File.Delete(_databasePath);
            }
        }
    }
}
