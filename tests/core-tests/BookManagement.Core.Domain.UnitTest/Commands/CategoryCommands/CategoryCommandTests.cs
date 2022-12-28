namespace BookManagement.Core.Domain.UnitTest.Commands.CategoryCommands;

[Collection(nameof(CategoryCommandTestsCollection))]
public class CategoryCommandTests
{
    private readonly CategoryCommandTestsFixture _fixture;

    public CategoryCommandTests(CategoryCommandTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Valid create command")]
    [Trait("Category", "Category Command")]
    public void CategoryCreateCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var createCategoryCommand = _fixture.GetValidCreateCategoryCommand();

        // Act
        var validationResult = createCategoryCommand.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Invalid create command")]
    [Trait("Category", "Category Command")]
    public void CategoryCreateCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var createCategoryCommand = _fixture.GetInvalidCreateCategoryCommand();

        // Act
        var validationResult = createCategoryCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Equal(2, validationResult.Errors.Count);
    }

    [Fact(DisplayName = "Valid update command")]
    [Trait("Category", "Category Command")]
    public void CategoryUpdateCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var updateCategoryCommand = _fixture.GetValidUpdateCategoryCommand();

        // Act
        var validationResult = updateCategoryCommand.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Invalid update command")]
    [Trait("Category", "Category Command")]
    public void CategoryUpdateCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var updateCategoryCommand = _fixture.GetInvalidUpdateCategoryCommand();

        // Act
        var validationResult = updateCategoryCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Equal(2, validationResult.Errors.Count);
    }

    [Fact(DisplayName = "Valid delete command")]
    [Trait("Category", "Category Command")]
    public void CategoryDeleteCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var deleteCategoryCommand = _fixture.GetValidDeleteCategoryCommand();

        // Act
        var validationResult = deleteCategoryCommand.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Invalid delete command")]
    [Trait("Category", "Category Command")]
    public void CategoryDeleteCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var deleteCategoryCommand = _fixture.GetInvalidDeleteCategoryCommand();

        // Act
        var validationResult = deleteCategoryCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
    }
}