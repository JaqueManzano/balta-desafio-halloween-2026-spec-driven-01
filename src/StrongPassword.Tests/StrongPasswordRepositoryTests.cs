using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StrongPassword.Api.Data;
using StrongPassword.Api.Repositories;
using StrongPasswordEntity = StrongPassword.Api.Models.StrongPassword;

namespace StrongPassword.Tests;

public sealed class StrongPasswordRepositoryTests
{
    [Fact]
    public async Task AddAsync_PersistsPasswordWithUniqueIdentifier()
    {
        await using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();
        var options = new DbContextOptionsBuilder<PasswordDbContext>()
            .UseSqlite(connection)
            .Options;

        await using var dbContext = new PasswordDbContext(options);
        await dbContext.Database.MigrateAsync();
        var repository = new StrongPasswordRepository(dbContext);
        var password = StrongPasswordEntity.Create("repository-test-password!");

        await repository.AddAsync(password);
        var result = await repository.GetByIdAsync(password.Id);

        Assert.NotNull(result);
        Assert.Equal(password.Id, result.Id);
        Assert.Equal(password.Password, result.Password);
    }

    [Fact]
    public async Task GetByIdAsync_FindsPasswordUsingNewDatabaseContext()
    {
        var databasePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.db");
        var options = new DbContextOptionsBuilder<PasswordDbContext>()
            .UseSqlite($"Data Source={databasePath};Pooling=False")
            .Options;
        var password = StrongPasswordEntity.Create("persistent-password-test!");

        try
        {
            await using (var writeContext = new PasswordDbContext(options))
            {
                await writeContext.Database.MigrateAsync();
                await new StrongPasswordRepository(writeContext).AddAsync(password);
            }

            StrongPasswordEntity? result;
            await using (var readContext = new PasswordDbContext(options))
            {
                result = await new StrongPasswordRepository(readContext).GetByIdAsync(password.Id);
            }

            Assert.NotNull(result);
            Assert.Equal(password.Password, result.Password);
        }
        finally
        {
            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }

            var walPath = $"{databasePath}-wal";
            if (File.Exists(walPath))
            {
                File.Delete(walPath);
            }

            var shmPath = $"{databasePath}-shm";
            if (File.Exists(shmPath))
            {
                File.Delete(shmPath);
            }
        }
    }
}
