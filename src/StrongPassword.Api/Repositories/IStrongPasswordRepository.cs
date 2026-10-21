using StrongPasswordEntity = StrongPassword.Api.Models.StrongPassword;

namespace StrongPassword.Api.Repositories;

public interface IStrongPasswordRepository
{
    Task AddAsync(StrongPasswordEntity password, CancellationToken cancellationToken = default);

    Task<StrongPasswordEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
