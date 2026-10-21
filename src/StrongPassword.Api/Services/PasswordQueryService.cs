using StrongPassword.Api.Repositories;
using StrongPasswordEntity = StrongPassword.Api.Models.StrongPassword;

namespace StrongPassword.Api.Services;

public sealed class PasswordQueryService(IStrongPasswordRepository repository) : IPasswordQueryService
{
    public Task<StrongPasswordEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return repository.GetByIdAsync(id, cancellationToken);
    }
}
