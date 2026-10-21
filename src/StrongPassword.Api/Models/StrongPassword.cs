namespace StrongPassword.Api.Models;

public sealed class StrongPassword
{
    private StrongPassword()
    {
    }

    private StrongPassword(Guid id, string password)
    {
        Id = id;
        Password = password;
    }

    public Guid Id { get; private set; }

    public string Password { get; private set; } = string.Empty;

    public static StrongPassword Create(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        if (password.Length < 16)
        {
            throw new ArgumentException("A senha deve conter pelo menos 16 caracteres.", nameof(password));
        }

        if (!password.Any(char.IsPunctuation) && !password.Any(char.IsSymbol))
        {
            throw new ArgumentException("A senha deve conter pelo menos um caractere especial.", nameof(password));
        }

        if (password.Any(char.IsWhiteSpace))
        {
            throw new ArgumentException("A senha não pode conter espaços em branco.", nameof(password));
        }

        return new StrongPassword(Guid.NewGuid(), password);
    }
}
