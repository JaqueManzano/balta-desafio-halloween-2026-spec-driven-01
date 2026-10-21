using StrongPassword.Api.Repositories;
using StrongPassword.Api.Services;
using StrongPasswordEntity = StrongPassword.Api.Models.StrongPassword;

namespace StrongPassword.Tests;

public sealed class PasswordServiceTests
{
    [Fact]
    public async Task CreateAsync_GeneratesPersistsAndReturnsPassword()
    {
        const string generatedPassword = "unit-test-generated!password";
        var repository = new InMemoryPasswordRepository();
        var service = new PasswordService(new FixedPasswordGenerator(generatedPassword), repository);

        var result = await service.CreateAsync();

        Assert.Equal(generatedPassword, result.Password);
        Assert.Equal(result, repository.StoredPassword);
    }

    private sealed class FixedPasswordGenerator(string password) : IPasswordGenerator
    {
        public string Generate() => password;
    }

    private sealed class InMemoryPasswordRepository : IStrongPasswordRepository
    {
        public StrongPasswordEntity? StoredPassword { get; private set; }

        public Task AddAsync(StrongPasswordEntity password, CancellationToken cancellationToken = default)
        {
            StoredPassword = password;
            return Task.CompletedTask;
        }

        public Task<StrongPasswordEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(StoredPassword?.Id == id ? StoredPassword : null);
        }
    }
}
