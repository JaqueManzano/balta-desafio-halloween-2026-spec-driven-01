using Microsoft.EntityFrameworkCore;
using StrongPasswordEntity = StrongPassword.Api.Models.StrongPassword;

namespace StrongPassword.Api.Data;

public sealed class PasswordDbContext(DbContextOptions<PasswordDbContext> options) : DbContext(options)
{
    public DbSet<StrongPasswordEntity> Passwords => Set<StrongPasswordEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StrongPasswordEntity>(entity =>
        {
            entity.ToTable("StrongPasswords");
            entity.HasKey(password => password.Id);
            entity.Property(password => password.Password).IsRequired();
        });
    }
}
