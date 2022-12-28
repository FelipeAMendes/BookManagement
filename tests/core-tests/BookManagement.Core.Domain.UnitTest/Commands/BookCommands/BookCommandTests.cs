using FluentAssertions;

namespace BookManagement.Core.Domain.UnitTest.Commands.BookCommands;

[Collection(nameof(BookCommandTestsCollection))]
public class BookCommandTests
{
    private readonly BookCommandTestsFixture _fixture;

    public BookCommandTests(BookCommandTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Valid create command")]
    [Trait("Category", "Book Command")]
    public void CreateBookCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var createBookCommand = _fixture.GetValidCreateBookCommand();

        // Act
        var validationResult = createBookCommand.Validate();

        // Assert
        createBookCommand.Quotes.Select(x => x.Validate().IsValid).Should().OnlyContain(x => true);
        createBookCommand.Reviews.Select(x => x.Validate().IsValid).Should().OnlyContain(x => true);
        createBookCommand.Keywords.Select(x => x.Validate().IsValid).Should().OnlyContain(x => true);
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Fact(DisplayName = "Invalid create command")]
    [Trait("Category", "Book Command")]
    public void CreateCategoryCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var createBookCommand = _fixture.GetInvalidCreateBookCommand();

        // Act
        var validationResult = createBookCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Equal(3, validationResult.Errors.Count);
    }

    [Fact(DisplayName = "Create command to Entity")]
    [Trait("Category", "Book Command")]
    public void CreateBookCommand_ToEntityMethodIsCalled_ReturnsEntity()
    {
        // Arrange
        var createBookCommand = _fixture.GetValidCreateBookCommand();

        // Act
        var entityCreated = createBookCommand.ToEntity();

        // Assert
        Assert.Equal(createBookCommand.Title, entityCreated.Title);
        Assert.Equal(createBookCommand.Description, entityCreated.Description);
        Assert.Equal(createBookCommand.PublicationDate, entityCreated.PublicationDate);
        Assert.Equal(createBookCommand.Format, entityCreated.Format);
    }

    [Fact(DisplayName = "Valid update command")]
    [Trait("Category", "Book Command")]
    public void UpdateBookCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var updateBookCommand = _fixture.GetValidUpdateBookCommand();

        // Act
        var validationResult = updateBookCommand.Validate();

        // Assert
        updateBookCommand.Quotes.Select(x => x.Validate().IsValid).Should().OnlyContain(x => true);
        updateBookCommand.Reviews.Select(x => x.Validate().IsValid).Should().OnlyContain(x => true);
        updateBookCommand.Keywords.Select(x => x.Validate().IsValid).Should().OnlyContain(x => true);
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Fact(DisplayName = "Invalid update command")]
    [Trait("Category", "Book Command")]
    public void UpdateBookCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var updateBookCommand = _fixture.GetInvalidUpdateBookCommand();

        // Act
        var validationResult = updateBookCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Equal(3, validationResult.Errors.Count);
    }

    [Fact(DisplayName = "Update command to Entity")]
    [Trait("Category", "Book Command")]
    public void UpdateBookCommand_ToEntityMethodIsCalled_ReturnsEntity()
    {
        // Arrange
        var updateBookCommand = _fixture.GetValidUpdateBookCommand();
        var book = _fixture.GetBookEntity();
        updateBookCommand.Id = book.Id;

        // Act
        var entityUpdated = updateBookCommand.UpdateEntity(book);

        // Assert
        Assert.Equal(updateBookCommand.Id, entityUpdated.Id);
        Assert.Equal(updateBookCommand.Title, entityUpdated.Title);
        Assert.Equal(updateBookCommand.Description, entityUpdated.Description);
        Assert.Equal(updateBookCommand.PublicationDate, entityUpdated.PublicationDate);
        Assert.Equal(updateBookCommand.Format, entityUpdated.Format);
    }

    [Fact(DisplayName = "Valid delete command")]
    [Trait("Category", "Book Command")]
    public void DeleteBookCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var deleteBookCommand = _fixture.GetValidDeleteBookCommand();

        // Act
        var validationResult = deleteBookCommand.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Invalid delete command")]
    [Trait("Category", "Book Command")]
    public void DeleteBookCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var deleteBookCommand = _fixture.GetInvalidDeleteBookCommand();

        // Act
        var validationResult = deleteBookCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
    }
}