using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StrongPassword.Api.Data;

#nullable disable

namespace StrongPassword.Api.Migrations;

[DbContext(typeof(PasswordDbContext))]
public partial class PasswordDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "9.0.10");

        modelBuilder.Entity("StrongPassword.Api.Models.StrongPassword", entity =>
        {
            entity.Property<Guid>("Id")
                .HasColumnType("TEXT");

            entity.Property<string>("Password")
                .IsRequired()
                .HasColumnType("TEXT");

            entity.HasKey("Id");

            entity.ToTable("StrongPasswords");
        });
    }
}
