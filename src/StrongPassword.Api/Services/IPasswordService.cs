using StrongPasswordEntity = StrongPassword.Api.Models.StrongPassword;

namespace StrongPassword.Api.Services;

public interface IPasswordService
{
    Task<StrongPasswordEntity> CreateAsync(CancellationToken cancellationToken = default);
}
