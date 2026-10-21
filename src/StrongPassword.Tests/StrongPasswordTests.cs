using StrongPasswordEntity = StrongPassword.Api.Models.StrongPassword;

namespace StrongPassword.Tests;

public sealed class StrongPasswordTests
{
    [Fact]
    public void Create_AssignsUniqueIdentifierAndKeepsPassword()
    {
        const string password = "correct-horse!battery";

        var first = StrongPasswordEntity.Create(password);
        var second = StrongPasswordEntity.Create(password);

        Assert.Equal(password, first.Password);
        Assert.NotEqual(Guid.Empty, first.Id);
        Assert.NotEqual(first.Id, second.Id);
    }

    [Theory]
    [InlineData("short!")]
    [InlineData("passwordwithoutspecial")]
    [InlineData("password with spaces!")]
    public void Create_RejectsPasswordThatDoesNotMeetRequirements(string password)
    {
        Assert.Throws<ArgumentException>(() => StrongPasswordEntity.Create(password));
    }
}
