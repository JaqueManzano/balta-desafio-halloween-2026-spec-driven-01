using System.Security.Cryptography;

namespace StrongPassword.Api.Services;

public sealed class PasswordGenerator : IPasswordGenerator
{
    private const string LettersAndDigits = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const string SpecialCharacters = "!@#$%^&*()-_=+[]{}?";
    private const int PasswordLength = 24;

    public string Generate()
    {
        var password = new char[PasswordLength];
        password[0] = LettersAndDigits[RandomNumberGenerator.GetInt32(LettersAndDigits.Length)];
        password[1] = SpecialCharacters[RandomNumberGenerator.GetInt32(SpecialCharacters.Length)];

        var availableCharacters = LettersAndDigits + SpecialCharacters;
        for (var index = 2; index < password.Length; index++)
        {
            password[index] = availableCharacters[RandomNumberGenerator.GetInt32(availableCharacters.Length)];
        }

        for (var index = password.Length - 1; index > 0; index--)
        {
            var swapIndex = RandomNumberGenerator.GetInt32(index + 1);
            (password[index], password[swapIndex]) = (password[swapIndex], password[index]);
        }

        return new string(password);
    }
}
