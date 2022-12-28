using static BookManagement.Core.Domain.Entities.Authors.Specifications.AuthorSpecification;

namespace BookManagement.Core.Domain.UnitTest.Commands.AuthorCommands;

[Collection(nameof(AuthorCommandTestsCollection))]
public class AuthorCommandTests
{
    private readonly AuthorCommandTestsFixture _fixture;

    public AuthorCommandTests(AuthorCommandTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Valid create command")]
    [Trait("Category", "Author Command")]
    public void AuthorCreateCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var createAuthorCommand = _fixture.GetValidCreateAuthorCommand();

        // Act
        var validationResult = createAuthorCommand.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Invalid create command")]
    [Trait("Category", "Author Command")]
    public void AuthorCreateCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var createAuthorCommand = _fixture.GetInvalidCreateAuthorCommand();

        // Act
        var validationResult = createAuthorCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Equal(2, validationResult.Errors.Count);
    }

    [Fact(DisplayName = "Valid update command")]
    [Trait("Category", "Author Command")]
    public void AuthorUpdateCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var updateAuthorCommand = _fixture.GetValidUpdateAuthorCommand();

        // Act
        var validationResult = updateAuthorCommand.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Invalid update command")]
    [Trait("Category", "Author Command")]
    public void AuthorUpdateCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var updateAuthorCommand = _fixture.GetInvalidUpdateAuthorCommand(NameColumnSize);

        // Act
        var validationResult = updateAuthorCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Equal(2, validationResult.Errors.Count);
    }

    [Fact(DisplayName = "Valid delete command")]
    [Trait("Category", "Author Command")]
    public void AuthorDeleteCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var deleteAuthorCommand = _fixture.GetValidDeleteAuthorCommand();

        // Act
        var validationResult = deleteAuthorCommand.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Invalid delete command")]
    [Trait("Category", "Author Command")]
    public void AuthorDeleteCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var deleteAuthorCommand = _fixture.GetInvalidDeleteAuthorCommand();

        // Act
        var validationResult = deleteAuthorCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
    }
}