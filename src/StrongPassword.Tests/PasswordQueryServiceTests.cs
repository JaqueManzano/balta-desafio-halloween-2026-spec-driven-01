using StrongPassword.Api.Repositories;
using StrongPassword.Api.Services;
using StrongPasswordEntity = StrongPassword.Api.Models.StrongPassword;

namespace StrongPassword.Tests;

public sealed class PasswordQueryServiceTests
{
    [Fact]
    public async Task GetByIdAsync_ReturnsPasswordForMatchingIdentifier()
    {
        var password = StrongPasswordEntity.Create("query-test-password!");
        var service = new PasswordQueryService(new InMemoryPasswordRepository(password));

        var result = await service.GetByIdAsync(password.Id);

        Assert.Equal(password, result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNullWhenIdentifierDoesNotExist()
    {
        var service = new PasswordQueryService(new InMemoryPasswordRepository(null));

        var result = await service.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    private sealed class InMemoryPasswordRepository(StrongPasswordEntity? password) : IStrongPasswordRepository
    {
        public Task AddAsync(StrongPasswordEntity newPassword, CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }

        public Task<StrongPasswordEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(password?.Id == id ? password : null);
        }
    }
}
