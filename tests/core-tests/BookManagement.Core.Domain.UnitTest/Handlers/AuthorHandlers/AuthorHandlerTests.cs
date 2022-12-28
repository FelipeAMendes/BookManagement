using BookManagement.Core.Domain.Entities.Authors;
using BookManagement.Core.Domain.Handlers.AuthorHandlers;
using BookManagement.Core.Shared.Handlers;
using Moq;

namespace BookManagement.Core.Domain.UnitTest.Handlers.AuthorHandlers;

[Collection(nameof(AuthorHandlerTestsCollection))]
public class AuthorHandlerTests
{
    private readonly AuthorHandlerTestsFixture _authorFixture;
    private readonly AuthorHandler _authorHandler;

    public AuthorHandlerTests(AuthorHandlerTestsFixture authorFixture)
    {
        _authorFixture = authorFixture;
        _authorHandler = _authorFixture.GetHandlerInstance();

        _authorFixture.CreateGetByIdRepositoryMock();
    }

    [Fact(DisplayName = "Author is not valid - create")]
    [Trait("Category", "Author Handler")]
    public async Task AuthorIsNotValid_HandleCreateIsCalled_ReturnsFalse()
    {
        //Arrange
        var createCommand = _authorFixture.GetInvalidCreateCommand();
        _authorFixture.GetCreateRepositoryMock(false);

        //Act
        var commandResult = await _authorHandler.HandleAsync(createCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Single(commandResult.Errors);
    }

    [Fact(DisplayName = "Author repository with error - create")]
    [Trait("Category", "Author Handler")]
    public async Task AuthorRepositoryWithError_HandleCreateIsCalled_ReturnsCreationError()
    {
        //Arrange
        var createCommand = _authorFixture.GetValidCreateCommand();
        var repositoryMock = _authorFixture.GetCreateRepositoryMock(false);

        //Act
        var commandResult = await _authorHandler.HandleAsync(createCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.CreationError, commandResult.ValidationType);

        repositoryMock.Verify(x => x.CreateAsync(It.IsAny<Author>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Create author")]
    [Trait("Category", "Author Handler")]
    public async Task AuthorIsValid_HandleCreateIsCalled_ReturnsSuccess()
    {
        //Arrange
        var createCommand = _authorFixture.GetValidCreateCommand();
        var repositoryMock = _authorFixture.GetCreateRepositoryMock(true);

        //Act
        var commandResult = await _authorHandler.HandleAsync(createCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);

        repositoryMock.Verify(x => x.CreateAsync(It.IsAny<Author>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Author is not valid - update")]
    [Trait("Category", "Author Handler")]
    public async Task AuthorIsNotValid_HandleUpdateIsCalled_ReturnsFalse()
    {
        //Arrange
        var updateCommand = _authorFixture.GetInvalidUpdateCommand();

        //Act
        var commandResult = await _authorHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Single(commandResult.Errors);
    }

    [Fact(DisplayName = "Author does not exist - update")]
    [Trait("Category", "Author Handler")]
    public async Task AuthorDoesNotExist_HandleUpdateIsCalled_ReturnsNotFound()
    {
        //Arrange
        var updateCommand = _authorFixture.GetValidUpdateCommand();
        _authorFixture.CreateNonExistingGetByIdRepositoryMock();

        //Act
        var commandResult = await _authorHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ItemNotFound, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Author repository with error - update")]
    [Trait("Category", "Author Handler")]
    public async Task AuthorRepositoryWithError_HandleUpdateIsCalled_ReturnsChangeError()
    {
        //Arrange
        var updateCommand = _authorFixture.GetValidUpdateCommand();
        var repositoryMock = _authorFixture.GetUpdateRepositoryMock(false);

        //Act
        var commandResult = await _authorHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ChangeError, commandResult.ValidationType);

        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Author>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Update author")]
    [Trait("Category", "Author Handler")]
    public async Task AuthorIsValid_HandleUpdateIsCalled_ReturnsSuccess()
    {
        //Arrange
        var updateCommand = _authorFixture.GetValidUpdateCommand();
        var repositoryMock = _authorFixture.GetUpdateRepositoryMock(true);

        //Act
        var commandResult = await _authorHandler.HandleAsync(updateCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);

        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Author>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Author does not exist - delete")]
    [Trait("Category", "Author Handler")]
    public async Task AuthorDoesNotExist_HandleDeleteIsCalled_ReturnsNotFound()
    {
        //Arrange
        var deleteCommand = _authorFixture.GetValidDeleteCommand();
        _authorFixture.CreateNonExistingGetByIdRepositoryMock();

        //Act
        var commandResult = await _authorHandler.HandleAsync(deleteCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ItemNotFound, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Author repository with error - delete")]
    [Trait("Category", "Author Handler")]
    public async Task AuthorRepositoryWithError_HandleDeleteIsCalled_ReturnsRemovalError()
    {
        //Arrange
        var deleteCommand = _authorFixture.GetValidDeleteCommand();
        var repositoryMock = _authorFixture.GetUpdateRepositoryMock(false);

        //Act
        var commandResult = await _authorHandler.HandleAsync(deleteCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.RemovalError, commandResult.ValidationType);

        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Author>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Delete author")]
    [Trait("Category", "Author Handler")]
    public async Task AuthorIsValid_HandleDeleteIsCalled_ReturnsSuccess()
    {
        //Arrange
        var deleteCommand = _authorFixture.GetValidDeleteCommand();
        var repositoryMock = _authorFixture.GetUpdateRepositoryMock(true);

        //Act
        var commandResult = await _authorHandler.HandleAsync(deleteCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);

        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Author>()), Times.AtLeastOnce);
    }
}