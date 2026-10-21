using Microsoft.EntityFrameworkCore;
using StrongPassword.Api.Data;
using StrongPassword.Api.Endpoints;
using StrongPassword.Api.Repositories;
using StrongPassword.Api.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=strong-passwords.db";

builder.Services.AddDbContext<PasswordDbContext>(options => options.UseSqlite(connectionString));
builder.Services.AddScoped<IStrongPasswordRepository, StrongPasswordRepository>();
builder.Services.AddSingleton<IPasswordGenerator, PasswordGenerator>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IPasswordQueryService, PasswordQueryService>();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PasswordDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.MapPasswordEndpoints();

app.Run();

public partial class Program;
