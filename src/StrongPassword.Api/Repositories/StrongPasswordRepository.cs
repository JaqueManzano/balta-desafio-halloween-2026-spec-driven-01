using Microsoft.EntityFrameworkCore;
using StrongPassword.Api.Data;
using StrongPasswordEntity = StrongPassword.Api.Models.StrongPassword;

namespace StrongPassword.Api.Repositories;

public sealed class StrongPasswordRepository(PasswordDbContext dbContext) : IStrongPasswordRepository
{
    public async Task AddAsync(StrongPasswordEntity password, CancellationToken cancellationToken = default)
    {
        await dbContext.Passwords.AddAsync(password, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<StrongPasswordEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Passwords.SingleOrDefaultAsync(password => password.Id == id, cancellationToken);
    }
}
