using StrongPassword.Api.Services;

namespace StrongPassword.Tests;

public sealed class PasswordGeneratorTests
{
    [Fact]
    public void Generate_ReturnsPasswordWithMinimumLengthSpecialCharacterAndNoWhitespace()
    {
        var generator = new PasswordGenerator();

        for (var attempt = 0; attempt < 100; attempt++)
        {
            var password = generator.Generate();

            Assert.True(password.Length >= 16);
            Assert.Contains(password, character => char.IsPunctuation(character) || char.IsSymbol(character));
            Assert.DoesNotContain(password, char.IsWhiteSpace);
        }
    }
}
