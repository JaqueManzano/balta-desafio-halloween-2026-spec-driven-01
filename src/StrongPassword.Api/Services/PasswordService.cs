using StrongPassword.Api.Repositories;
using StrongPasswordEntity = StrongPassword.Api.Models.StrongPassword;

namespace StrongPassword.Api.Services;

public sealed class PasswordService(
    IPasswordGenerator passwordGenerator,
    IStrongPasswordRepository repository) : IPasswordService
{
    public async Task<StrongPasswordEntity> CreateAsync(CancellationToken cancellationToken = default)
    {
        var password = StrongPasswordEntity.Create(passwordGenerator.Generate());
        await repository.AddAsync(password, cancellationToken);
        return password;
    }
}
