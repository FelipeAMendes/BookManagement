using BookManagement.Core.Domain.Entities.Publishers;
using BookManagement.Core.Domain.Handlers.PublisherHandlers;
using BookManagement.Core.Shared.Handlers;
using Moq;

namespace BookManagement.Core.Domain.UnitTest.Handlers.PublisherHandlers;

[Collection(nameof(PublisherHandlerTestsCollection))]
public class PublisherHandlerTests
{
    private readonly PublisherHandlerTestsFixture _publisherFixture;
    private readonly PublisherHandler _publisherHandler;

    public PublisherHandlerTests(PublisherHandlerTestsFixture publisherFixture)
    {
        _publisherFixture = publisherFixture;
        _publisherHandler = _publisherFixture.GetHandlerInstance();

        _publisherFixture.CreateGetByIdRepositoryMock();
    }

    [Fact(DisplayName = "Publisher is not valid - create")]
    [Trait("Category", "Publisher Handler")]
    public async Task PublisherIsNotValid_HandleCreateIsCalled_ReturnsFalse()
    {
        //Arrange
        var createCommand = _publisherFixture.GetInvalidCreateCommand();
        _publisherFixture.GetCreateRepositoryMock(false);

        //Act
        var commandResult = await _publisherHandler.HandleAsync(createCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Single(commandResult.Errors);
    }

    [Fact(DisplayName = "Publisher repository with error - create")]
    [Trait("Category", "Publisher Handler")]
    public async Task PublisherRepositoryWithError_HandleCreateIsCalled_ReturnsCreationError()
    {
        //Arrange
        var createCommand = _publisherFixture.GetValidCreateCommand();
        var repositoryMock = _publisherFixture.GetCreateRepositoryMock(false);

        //Act
        var commandResult = await _publisherHandler.HandleAsync(createCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.CreationError, commandResult.ValidationType);

        repositoryMock.Verify(x => x.CreateAsync(It.IsAny<Publisher>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Create publisher")]
    [Trait("Category", "Publisher Handler")]
    public async Task PublisherIsValid_HandleCreateIsCalled_ReturnsSuccess()
    {
        //Arrange
        var createCommand = _publisherFixture.GetValidCreateCommand();
        var repositoryMock = _publisherFixture.GetCreateRepositoryMock(true);

        //Act
        var commandResult = await _publisherHandler.HandleAsync(createCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);

        repositoryMock.Verify(x => x.CreateAsync(It.IsAny<Publisher>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Publisher is not valid - update")]
    [Trait("Category", "Publisher Handler")]
    public async Task PublisherIsNotValid_HandleUpdateIsCalled_ReturnsFalse()
    {
        //Arrange
        var updateCommand = _publisherFixture.GetInvalidUpdateCommand();

        //Act
        var commandResult = await _publisherHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Single(commandResult.Errors);
    }

    [Fact(DisplayName = "Publisher does not exist - update")]
    [Trait("Category", "Publisher Handler")]
    public async Task PublisherDoesNotExist_HandleUpdateIsCalled_ReturnsNotFound()
    {
        //Arrange
        var updateCommand = _publisherFixture.GetValidUpdateCommand();
        _publisherFixture.CreateNonExistingGetByIdRepositoryMock();

        //Act
        var commandResult = await _publisherHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ItemNotFound, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Publisher repository with error - update")]
    [Trait("Category", "Publisher Handler")]
    public async Task PublisherRepositoryWithError_HandleUpdateIsCalled_ReturnsChangeError()
    {
        //Arrange
        var updateCommand = _publisherFixture.GetValidUpdateCommand();
        var repositoryMock = _publisherFixture.GetUpdateRepositoryMock(false);

        //Act
        var commandResult = await _publisherHandler.HandleAsync(updateCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ChangeError, commandResult.ValidationType);

        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Publisher>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Update publisher")]
    [Trait("Category", "Publisher Handler")]
    public async Task PublisherIsValid_HandleUpdateIsCalled_ReturnsSuccess()
    {
        //Arrange
        var updateCommand = _publisherFixture.GetValidUpdateCommand();
        var repositoryMock = _publisherFixture.GetUpdateRepositoryMock(true);

        //Act
        var commandResult = await _publisherHandler.HandleAsync(updateCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);

        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Publisher>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Publisher does not exist - delete")]
    [Trait("Category", "Publisher Handler")]
    public async Task PublisherDoesNotExist_HandleDeleteIsCalled_ReturnsFalse()
    {
        //Arrange
        var deleteCommand = _publisherFixture.GetValidDeleteCommand();
        _publisherFixture.CreateNonExistingGetByIdRepositoryMock();

        //Act
        var commandResult = await _publisherHandler.HandleAsync(deleteCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ItemNotFound, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Publisher repository with error - delete")]
    [Trait("Category", "Publisher Handler")]
    public async Task PublisherRepositoryWithError_HandleDeleteIsCalled_ReturnsRemovalError()
    {
        //Arrange
        var deleteCommand = _publisherFixture.GetValidDeleteCommand();
        var repositoryMock = _publisherFixture.GetUpdateRepositoryMock(false);

        //Act
        var commandResult = await _publisherHandler.HandleAsync(deleteCommand);

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.RemovalError, commandResult.ValidationType);

        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Publisher>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Delete publisher")]
    [Trait("Category", "Publisher Handler")]
    public async Task PublisherIsValid_HandleDeleteIsCalled_ReturnsSuccess()
    {
        //Arrange
        var deleteCommand = _publisherFixture.GetValidDeleteCommand();
        var repositoryMock = _publisherFixture.GetUpdateRepositoryMock(true);

        //Act
        var commandResult = await _publisherHandler.HandleAsync(deleteCommand);

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);

        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Publisher>()), Times.AtLeastOnce);
    }
}