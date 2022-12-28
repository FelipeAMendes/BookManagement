using AutoFixture;
using Bogus;
using BookManagement.Core.Domain.Commands.AuthorCommands;
using BookManagement.Core.Domain.Entities.Authors;
using BookManagement.Core.Domain.Handlers.AuthorHandlers;
using BookManagement.Core.Domain.Repositories.AuthorRepositories.Interfaces;
using Moq;
using Moq.AutoMock;
using static BookManagement.Core.Domain.Entities.Authors.Specifications.AuthorSpecification;

namespace BookManagement.Core.Domain.UnitTest.Handlers.AuthorHandlers;

[CollectionDefinition(nameof(AuthorHandlerTestsCollection))]
public class AuthorHandlerTestsCollection : ICollectionFixture<AuthorHandlerTestsFixture> { }

public class AuthorHandlerTestsFixture
{
    private readonly IFixture _autoFixture;
    private readonly AutoMocker _autoMocker;

    public AuthorHandlerTestsFixture()
    {
        _autoMocker = new AutoMocker();
        _autoFixture = new Fixture();
    }

    public AuthorHandler GetHandlerInstance()
    {
        return _autoMocker.CreateInstance<AuthorHandler>();
    }

    public CreateAuthorCommand GetValidCreateCommand()
    {
        return _autoFixture.Create<CreateAuthorCommand>();
    }

    public CreateAuthorCommand GetInvalidCreateCommand()
    {
        return new Faker<CreateAuthorCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String(NameColumnSize + 1, NameColumnSize * 2);

                return new CreateAuthorCommand
                {
                    Name = name
                };
            });
    }

    public Mock<IAuthorRepository> GetCreateRepositoryMock(bool success)
    {
        var authorRepository = _autoMocker.GetMock<IAuthorRepository>();

        authorRepository
            .Setup(x => x.CreateAsync(It.IsAny<Author>()))
            .ReturnsAsync(success);

        return authorRepository;
    }

    public void CreateGetByIdRepositoryMock()
    {
        _autoMocker
            .GetMock<IAuthorRepository>()
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Author("AuthorName", "AuthorDescription"));
    }

    public void CreateNonExistingGetByIdRepositoryMock()
    {
        _autoMocker
            .GetMock<IAuthorRepository>()
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);
    }

    public UpdateAuthorCommand GetValidUpdateCommand()
    {
        return _autoFixture.Create<UpdateAuthorCommand>();
    }

    public UpdateAuthorCommand GetInvalidUpdateCommand()
    {
        return new Faker<UpdateAuthorCommand>()
            .CustomInstantiator(x =>
            {
                var id = x.Random.Guid();
                var name = x.Random.String(NameColumnSize, NameColumnSize * 2);

                return new UpdateAuthorCommand
                {
                    Id = id,
                    Name = name
                };
            });
    }

    public Mock<IAuthorRepository> GetUpdateRepositoryMock(bool success)
    {
        var authorRepository = _autoMocker.GetMock<IAuthorRepository>();

        authorRepository
            .Setup(x => x.UpdateAsync(It.IsAny<Author>()))
            .ReturnsAsync(success);

        return authorRepository;
    }

    public DeleteAuthorCommand GetValidDeleteCommand()
    {
        return _autoFixture.Create<DeleteAuthorCommand>();
    }
}