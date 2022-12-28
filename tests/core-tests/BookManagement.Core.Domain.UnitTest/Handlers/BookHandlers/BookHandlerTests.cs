using BookManagement.Core.Domain.Handlers.BookHandlers;
using BookManagement.Core.Shared.Handlers;

namespace BookManagement.Core.Domain.UnitTest.Handlers.BookHandlers;

[Collection(nameof(BookHandlerTestsCollection))]
public class BookHandlerTests
{
    private readonly BookHandlerTestsFixture _bookFixture;
    private readonly BookHandler _bookHandler;

    public BookHandlerTests(BookHandlerTestsFixture bookFixture)
    {
        _bookFixture = bookFixture;
        _bookHandler = bookFixture.GetHandlerInstance();

        _bookFixture.CreateGetByIdRepositoryMock();
        _bookFixture.CreateGetByIdCategoryRepositoryMock(true);
        _bookFixture.CreateGetByIdPublisherRepositoryMock(true);
    }

    [Fact(DisplayName = "Book is not valid - create")]
    [Trait("Category", "Book Handler")]
    public async Task BookIsNotValid_HandleCreateIsCalled_ReturnsFalse()
    {
        //Arrange
        var createCommand = _bookFixture.GetInvalidCreateCommand();

        //Act
        var commandResult = await _bookHandler.HandleAsync(createCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(2, commandResult.Errors.Count());
    }

    [Fact(DisplayName = "Book repository with error - create")]
    [Trait("Category", "Book Handler")]
    public async Task BookRepositoryWithError_HandleCreateIsCalled_ReturnsCreationError()
    {
        //Arrange
        var createCommand = _bookFixture.GetValidCreateCommand();
        _bookFixture.GetCreateRepositoryMock(false);

        //Act
        var commandResult = await _bookHandler.HandleAsync(createCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.CreationError, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Create book")]
    [Trait("Category", "Book Handler")]
    public async Task BookIsValid_HandleCreateIsCalled_ReturnsSuccess()
    {
        //Arrange
        var createCommand = _bookFixture.GetValidCreateCommand();
        _bookFixture.GetCreateRepositoryMock(true);

        //Act
        var commandResult = await _bookHandler.HandleAsync(createCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);
    }

    [Fact(DisplayName = "Book does not exist - update")]
    [Trait("Category", "Book Handler")]
    public async Task BookDoesNotExist_HandleUpdateIsCalled_ReturnsNotFound()
    {
        //Arrange
        var updateCommand = _bookFixture.GetValidUpdateCommand();
        _bookFixture.CreateNonExistingGetByIdRepositoryMock();

        //Act
        var commandResult = await _bookHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ItemNotFound, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Category Book does not exist - update")]
    [Trait("Category", "Book Handler")]
    public async Task CategoryBookDoesNotExist_HandleUpdateIsCalled_ReturnsFalse()
    {
        //Arrange
        var updateCommand = _bookFixture.GetValidUpdateCommand();
        _bookFixture.CreateGetByIdCategoryRepositoryMock(false);

        //Act
        var commandResult = await _bookHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Single(commandResult.Errors);
    }

    [Fact(DisplayName = "Book with same category - update")]
    [Trait("Category", "Book Handler")]
    public async Task BookWithSameCategory_HandleUpdateIsCalled_DontGoToDatabase()
    {
        //Arrange
        _bookFixture.GetUpdateRepositoryMock(true);
        var book = _bookFixture.CreateGetByIdRepositoryMock();
        var updateCommand = _bookFixture.GetValidUpdateCommand(book.Category.Id);

        //Act
        var commandResult = await _bookHandler.HandleAsync(updateCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);
        Assert.Equal(book.Category.Id, updateCommand.CategoryId);
    }

    [Fact(DisplayName = "Publisher Book does not exist - update")]
    [Trait("Category", "Book Handler")]
    public async Task PublisherBookDoesNotExist_HandleUpdateIsCalled_ReturnsFalse()
    {
        //Arrange
        var updateCommand = _bookFixture.GetValidUpdateCommand();
        _bookFixture.CreateGetByIdPublisherRepositoryMock(false);

        //Act
        var commandResult = await _bookHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Single(commandResult.Errors);
    }

    [Fact(DisplayName = "Book with same publisher - update")]
    [Trait("Category", "Book Handler")]
    public async Task BookWithSamePublisher_HandleUpdateIsCalled_DontGoToDatabase()
    {
        //Arrange
        _bookFixture.GetUpdateRepositoryMock(true);
        var book = _bookFixture.CreateGetByIdRepositoryMock();
        var updateCommand = _bookFixture.GetValidUpdateCommand(null, book.Publisher.Id);

        //Act
        var commandResult = await _bookHandler.HandleAsync(updateCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);
        Assert.Equal(book.Publisher.Id, updateCommand.PublisherId);
    }

    [Fact(DisplayName = "Book is not valid - update")]
    [Trait("Category", "Book Handler")]
    public async Task BookIsNotValid_HandleUpdateIsCalled_ReturnsFalse()
    {
        //Arrange
        var updateCommand = _bookFixture.GetInvalidUpdateCommand();

        //Act
        var commandResult = await _bookHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(2, commandResult.Errors.Count());
    }

    [Fact(DisplayName = "Book repository with error - update")]
    [Trait("Category", "Book Handler")]
    public async Task BookRepositoryWithError_HandleUpdateIsCalled_ReturnsChangeError()
    {
        //Arrange
        var updateCommand = _bookFixture.GetValidUpdateCommand();
        _bookFixture.GetUpdateRepositoryMock(false);

        //Act
        var commandResult = await _bookHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ChangeError, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Update book")]
    [Trait("Category", "Book Handler")]
    public async Task BookIsValid_HandleUpdateIsCalled_ReturnsSuccess()
    {
        //Arrange
        var updateCommand = _bookFixture.GetValidUpdateCommand();
        _bookFixture.GetUpdateRepositoryMock(true);

        //Act
        var commandResult = await _bookHandler.HandleAsync(updateCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);
    }

    [Fact(DisplayName = "Book does not exist - delete")]
    [Trait("Category", "Book Handler")]
    public async Task BookDoesNotExist_HandleDeleteIsCalled_ReturnsNotFound()
    {
        //Arrange
        var deleteCommand = _bookFixture.GetValidDeleteCommand();
        _bookFixture.CreateNonExistingGetByIdRepositoryMock();

        //Act
        var commandResult = await _bookHandler.HandleAsync(deleteCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ItemNotFound, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Book repository with error - delete")]
    [Trait("Category", "Book Handler")]
    public async Task BookRepositoryWithError_HandleDeleteIsCalled_ReturnsRemovalError()
    {
        //Arrange
        var deleteCommand = _bookFixture.GetValidDeleteCommand();
        _bookFixture.GetUpdateRepositoryMock(false);

        //Act
        var commandResult = await _bookHandler.HandleAsync(deleteCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.RemovalError, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Delete book")]
    [Trait("Category", "Book Handler")]
    public async Task BookIsValid_HandleDeleteIsCalled_ReturnsSuccess()
    {
        //Arrange
        var deleteCommand = _bookFixture.GetValidDeleteCommand();
        _bookFixture.GetUpdateRepositoryMock(true);

        //Act
        var commandResult = await _bookHandler.HandleAsync(deleteCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);
    }
}