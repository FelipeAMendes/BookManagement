using BookManagement.Core.Domain.Handlers.CategoryHandlers;
using BookManagement.Core.Shared.Handlers;

namespace BookManagement.Core.Domain.UnitTest.Handlers.CategoryHandlers;

[Collection(nameof(CategoryHandlerTestsCollection))]
public class CategoryHandlerTests
{
    private readonly CategoryHandler _categoryHandler;
    private readonly CategoryHandlerTestsFixture _createFixture;

    public CategoryHandlerTests(CategoryHandlerTestsFixture createFixture)
    {
        _createFixture = createFixture;
        _categoryHandler = createFixture.GetHandlerInstance();

        _createFixture.CreateGetByIdRepositoryMock();
        _createFixture.CreateQueryResultAutoMapperMock();
    }

    [Fact(DisplayName = "Category is not valid - create")]
    [Trait("Category", "Category Handler")]
    public async Task CategoryIsNotValid_HandleCreateIsCalled_ReturnsFalse()
    {
        //Arrange
        var createCommand = _createFixture.GetInvalidCreateCommand();

        //Act
        var commandResult = await _categoryHandler.HandleAsync(createCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(2, commandResult.Errors.Count());
    }

    [Fact(DisplayName = "Category repository with error - create")]
    [Trait("Category", "Category Handler")]
    public async Task CategoryRepositoryWithError_HandleCreateIsCalled_ReturnsCreationError()
    {
        //Arrange
        var createCommand = _createFixture.GetValidCreateCommand(null);
        _createFixture.GetCreateRepositoryMock(false);

        //Act
        var commandResult = await _categoryHandler.HandleAsync(createCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.CreationError, commandResult.ValidationType);
    }

    [Theory(DisplayName = "Create category")]
    [Trait("Category", "Category Handler")]
    [InlineData(null)]
    [InlineData("81A75B60-7180-4B63-AA7F-2665DBC00412")]
    public async Task CategoryIsValid_HandleCreateIsCalled_ReturnsSuccess(string parentCategoryIdInput)
    {
        //Arrange
        _ = Guid.TryParse(parentCategoryIdInput, out var parentCategoryId);
        var createCommand = _createFixture.GetValidCreateCommand(parentCategoryId);
        _createFixture.GetCreateRepositoryMock(true);

        //Act
        var commandResult = await _categoryHandler.HandleAsync(createCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);
    }

    [Fact(DisplayName = "Category with invalid parent - create")]
    [Trait("Category", "Category Handler")]
    public async Task CategoryWithInvalidParent_HandleCreateCategoryIsCalled_ReturnsFalse()
    {
        //Arrange
        var createCommand = _createFixture.GetValidCreateCommand(Guid.Empty);
        _createFixture.CreateNonExistingGetByIdRepositoryMock();

        //Act
        var commandResult = await _categoryHandler.HandleAsync(createCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Single(commandResult.Errors);
    }

    [Fact(DisplayName = "Category is not valid - update")]
    [Trait("Category", "Category Handler")]
    public async Task CategoryIsNotValid_HandleUpdateIsCalled_ReturnsFalse()
    {
        //Arrange
        var updateCommand = _createFixture.GetInvalidUpdateCommand();

        //Act
        var commandResult = await _categoryHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Single(commandResult.Errors);
    }

    [Fact(DisplayName = "Category does not exist - update")]
    [Trait("Category", "Category Handler")]
    public async Task CategoryDoesNotExist_HandleUpdateIsCalled_ReturnsNotFound()
    {
        //Arrange
        var updateCommand = _createFixture.GetValidUpdateCommand(null);
        _createFixture.CreateNonExistingGetByIdRepositoryMock();

        //Act
        var commandResult = await _categoryHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ItemNotFound, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Category repository with error - update")]
    [Trait("Category", "Category Handler")]
    public async Task CategoryRepositoryWithError_HandleUpdateIsCalled_ReturnsChangeError()
    {
        //Arrange
        var updateCommand = _createFixture.GetValidUpdateCommand(null);
        _createFixture.GetUpdateRepositoryMock(false);

        //Act
        var commandResult = await _categoryHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ChangeError, commandResult.ValidationType);
    }

    [Theory(DisplayName = "Update category")]
    [Trait("Category", "Category Handler")]
    [InlineData(null)]
    [InlineData("81A75B60-7180-4B63-AA7F-2665DBC00412")]
    public async Task CategoryIsValid_HandleUpdateIsCalled_ReturnsSuccess(string parentCategoryIdInput)
    {
        //Arrange
        _ = Guid.TryParse(parentCategoryIdInput, out var parentCategoryId);

        var updateCommand = _createFixture.GetValidUpdateCommand(parentCategoryId);
        _createFixture.GetUpdateRepositoryMock(true);

        //Act
        var commandResult = await _categoryHandler.HandleAsync(updateCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);
    }

    [Fact(DisplayName = "Category does not exist - delete")]
    [Trait("Category", "Category Handler")]
    public async Task CategoryDoesNotExist_HandleDeleteIsCalled_ReturnsNotFound()
    {
        //Arrange
        var deleteCommand = _createFixture.GetValidDeleteCommand();
        _createFixture.CreateNonExistingGetByIdRepositoryMock();

        //Act
        var commandResult = await _categoryHandler.HandleAsync(deleteCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ItemNotFound, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Category repository with error - delete")]
    [Trait("Category", "Category Handler")]
    public async Task CategoryRepositoryWithError_HandleDeleteIsCalled_ReturnsRemovalError()
    {
        //Arrange
        var deleteCommand = _createFixture.GetValidDeleteCommand();
        _createFixture.GetUpdateRepositoryMock(false);

        //Act
        var commandResult = await _categoryHandler.HandleAsync(deleteCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.RemovalError, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Delete category")]
    [Trait("Category", "Category Handler")]
    public async Task CategoryIsValid_HandleDeleteIsCalled_ReturnsSuccess()
    {
        //Arrange
        var deleteCommand = _createFixture.GetValidDeleteCommand();
        _createFixture.GetUpdateRepositoryMock(true);

        //Act
        var commandResult = await _categoryHandler.HandleAsync(deleteCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);
    }
}