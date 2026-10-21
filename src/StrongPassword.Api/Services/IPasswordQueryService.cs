using StrongPasswordEntity = StrongPassword.Api.Models.StrongPassword;

namespace StrongPassword.Api.Services;

public interface IPasswordQueryService
{
    Task<StrongPasswordEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
