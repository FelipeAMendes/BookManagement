using AutoFixture;
using Bogus;
using BookManagement.Core.Domain.Commands.PublisherCommands;
using BookManagement.Core.Domain.Entities.Publishers;
using BookManagement.Core.Domain.Handlers.PublisherHandlers;
using BookManagement.Core.Domain.Repositories.PublisherRepositories.Interfaces;
using Moq;
using Moq.AutoMock;
using static BookManagement.Core.Domain.Entities.Publishers.Specifications.PublisherSpecification;

namespace BookManagement.Core.Domain.UnitTest.Handlers.PublisherHandlers;

[CollectionDefinition(nameof(PublisherHandlerTestsCollection))]
public class PublisherHandlerTestsCollection : ICollectionFixture<PublisherHandlerTestsFixture>
{
}

public class PublisherHandlerTestsFixture
{
    private readonly Fixture _autoFixture;
    private readonly AutoMocker _autoMocker;

    public PublisherHandlerTestsFixture()
    {
        _autoMocker = new AutoMocker();
        _autoFixture = new Fixture();
    }

    public PublisherHandler GetHandlerInstance()
    {
        return _autoMocker.CreateInstance<PublisherHandler>();
    }

    public CreatePublisherCommand GetValidCreateCommand()
    {
        return _autoFixture.Create<CreatePublisherCommand>();
    }

    public CreatePublisherCommand GetInvalidCreateCommand()
    {
        return new Faker<CreatePublisherCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String(NameColumnSize, NameColumnSize * 2);

                return new CreatePublisherCommand
                {
                    Name = name
                };
            });
    }

    public Mock<IPublisherRepository> GetCreateRepositoryMock(bool success)
    {
        var publisherRepository = _autoMocker.GetMock<IPublisherRepository>();

        publisherRepository
            .Setup(x => x.CreateAsync(It.IsAny<Publisher>()))
            .ReturnsAsync(success);

        return publisherRepository;
    }

    public void CreateGetByIdRepositoryMock()
    {
        _autoMocker
            .GetMock<IPublisherRepository>()
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Publisher("PublisherName", "PublisherDescription"));
    }

    public void CreateNonExistingGetByIdRepositoryMock()
    {
        _autoMocker
            .GetMock<IPublisherRepository>()
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);
    }

    public UpdatePublisherCommand GetValidUpdateCommand()
    {
        return _autoFixture.Create<UpdatePublisherCommand>();
    }

    public UpdatePublisherCommand GetInvalidUpdateCommand()
    {
        return new Faker<UpdatePublisherCommand>()
            .CustomInstantiator(x =>
            {
                var id = x.Random.Guid();
                var name = x.Random.String(NameColumnSize + 1, NameColumnSize * 2);

                return new UpdatePublisherCommand
                {
                    Id = id,
                    Name = name
                };
            });
    }

    public Mock<IPublisherRepository> GetUpdateRepositoryMock(bool success)
    {
        var publisherRepository = _autoMocker.GetMock<IPublisherRepository>();

        publisherRepository
            .Setup(x => x.UpdateAsync(It.IsAny<Publisher>()))
            .ReturnsAsync(success);

        return publisherRepository;
    }

    public DeletePublisherCommand GetValidDeleteCommand()
    {
        return _autoFixture.Create<DeletePublisherCommand>();
    }
}