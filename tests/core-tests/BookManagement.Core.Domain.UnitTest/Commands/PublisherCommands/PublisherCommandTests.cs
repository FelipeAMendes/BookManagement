namespace BookManagement.Core.Domain.UnitTest.Commands.PublisherCommands;

[Collection(nameof(PublisherCommandTestsCollection))]
public class PublisherCommandTests
{
    private readonly PublisherCommandTestsFixture _fixture;

    public PublisherCommandTests(PublisherCommandTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Valid create command")]
    [Trait("Category", "Publisher Command")]
    public void PublisherCreateCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var createPublisherCommand = _fixture.GetValidCreatePublisherCommand();

        // Act
        var validationResult = createPublisherCommand.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Invalid create command")]
    [Trait("Category", "Publisher Command")]
    public void PublisherCreateCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var createPublisherCommand = _fixture.GetInvalidCreatePublisherCommand();

        // Act
        var validationResult = createPublisherCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Equal(2, validationResult.Errors.Count);
    }

    [Fact(DisplayName = "Valid update command")]
    [Trait("Category", "Publisher Command")]
    public void PublisherUpdateCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var updatePublisherCommand = _fixture.GetValidUpdatePublisherCommand();

        // Act
        var validationResult = updatePublisherCommand.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Invalid update command")]
    [Trait("Category", "Publisher Command")]
    public void PublisherUpdateCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var updatePublisherCommand = _fixture.GetInvalidUpdatePublisherCommand();

        // Act
        var validationResult = updatePublisherCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Equal(2, validationResult.Errors.Count);
    }

    [Fact(DisplayName = "Valid delete command")]
    [Trait("Category", "Publisher Command")]
    public void PublisherDeleteCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var deletePublisherCommand = _fixture.GetValidDeletePublisherCommand();

        // Act
        var validationResult = deletePublisherCommand.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Invalid delete command")]
    [Trait("Category", "Publisher Command")]
    public void PublisherDeleteCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var deletePublisherCommand = _fixture.GetInvalidDeletePublisherCommand();

        // Act
        var validationResult = deletePublisherCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
    }
}