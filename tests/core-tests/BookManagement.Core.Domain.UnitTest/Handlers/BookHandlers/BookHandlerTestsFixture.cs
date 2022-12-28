using AutoFixture;
using Bogus;
using BookManagement.Core.Domain.Commands.BookCommands;
using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Categories;
using BookManagement.Core.Domain.Entities.Publishers;
using BookManagement.Core.Domain.Handlers.BookHandlers;
using BookManagement.Core.Domain.Repositories.BookRepositories.Interfaces;
using BookManagement.Core.Domain.Repositories.CategoryRepositories.Interfaces;
using BookManagement.Core.Domain.Repositories.PublisherRepositories.Interfaces;
using Moq;
using Moq.AutoMock;

namespace BookManagement.Core.Domain.UnitTest.Handlers.BookHandlers;

[CollectionDefinition(nameof(BookHandlerTestsCollection))]
public class BookHandlerTestsCollection : ICollectionFixture<BookHandlerTestsFixture> { }

public class BookHandlerTestsFixture
{
    private readonly IFixture _autoFixture;
    private readonly AutoMocker _autoMocker;

    public BookHandlerTestsFixture()
    {
        _autoMocker = new AutoMocker();
        _autoFixture = new Fixture();
    }

    public BookHandler GetHandlerInstance()
    {
        return _autoMocker.CreateInstance<BookHandler>();
    }

    public CreateBookCommand GetValidCreateCommand()
    {
        return new Faker<CreateBookCommand>()
            .CustomInstantiator(x =>
            {
                var title = x.Random.Word();
                var description = x.Lorem.Paragraph();
                var publicationDate = DateTime.Now;
                var format = x.Random.Enum<Format>();

                return new CreateBookCommand
                {
                    Title = title,
                    Description = description,
                    PublicationDate = publicationDate,
                    Format = format,
                    Isbn10 = "5734395951",
                    Isbn13 = "9781234567897",
                    CategoryId = x.Random.Guid(),
                    PublisherId = x.Random.Guid()
                };
            });
    }

    public CreateBookCommand GetInvalidCreateCommand()
    {
        return new Faker<CreateBookCommand>()
            .CustomInstantiator(x => new CreateBookCommand
            {
                PublicationDate = DateTime.Now,
                CategoryId = x.Random.Guid(),
                PublisherId = x.Random.Guid()
            });
    }

    public Mock<IBookRepository> GetCreateRepositoryMock(bool success)
    {
        var repositoryMock = _autoMocker.GetMock<IBookRepository>();

        repositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<Book>()))
            .ReturnsAsync(success);

        return repositoryMock;
    }

    public void CreateNonExistingGetByIdRepositoryMock()
    {
        _autoMocker
            .GetMock<IBookRepository>()
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);
    }

    public Book CreateGetByIdRepositoryMock()
    {
        var book = GetBookFaker();
        _autoMocker
            .GetMock<IBookRepository>()
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(book);

        return book;
    }

    public void CreateGetByIdCategoryRepositoryMock(bool returnCategory)
    {
        _autoMocker
            .GetMock<ICategoryRepository>()
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(returnCategory ? new Category("CategoryName", "CategoryDescription") : null);
    }

    public void CreateGetByIdPublisherRepositoryMock(bool returnPublisher)
    {
        _autoMocker
            .GetMock<IPublisherRepository>()
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(returnPublisher ? new Publisher("PublisherName", "PublisherDescription") : null);
    }

    public UpdateBookCommand GetValidUpdateCommand(Guid? categoryId = null, Guid? publisherId = null)
    {
        return new Faker<UpdateBookCommand>()
            .CustomInstantiator(x =>
            {
                var title = x.Random.Word();
                var description = x.Lorem.Paragraph();
                var publicationDate = DateTime.Now;
                var format = x.Random.Enum<Format>();

                return new UpdateBookCommand
                {
                    Id = x.Random.Guid(),
                    Title = title,
                    Description = description,
                    PublicationDate = publicationDate,
                    Format = format,
                    Isbn10 = "5734395951",
                    Isbn13 = "9781234567897",
                    CategoryId = categoryId ?? x.Random.Guid(),
                    PublisherId = publisherId ?? x.Random.Guid()
                };
            });
    }

    public UpdateBookCommand GetInvalidUpdateCommand()
    {
        return new Faker<UpdateBookCommand>()
            .CustomInstantiator(x => new UpdateBookCommand
            {
                Id = x.Random.Guid(),
                PublicationDate = DateTime.Now,
                CategoryId = x.Random.Guid(),
                PublisherId = x.Random.Guid()
            });
    }

    public Book GetBookFaker()
    {
        var book = new Faker<Book>()
            .CustomInstantiator(x =>
            {
                var book = new Book
                (
                    x.Name.FullName(),
                    x.Name.FullName(),
                    DateTime.Now,
                    Format.Hardcover,
                    null,
                    null
                );

                book.SetCategory(new Category(x.Name.FullName(), x.Name.FullName()));
                book.SetPublisher(new Publisher(x.Name.FullName(), x.Name.FullName()));

                return book;
            });

        return book;
    }

    public Mock<IBookRepository> GetUpdateRepositoryMock(bool success)
    {
        var repositoryMock = _autoMocker.GetMock<IBookRepository>();

        repositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Book>()))
            .ReturnsAsync(success);

        return repositoryMock;
    }

    public DeleteBookCommand GetValidDeleteCommand()
    {
        return _autoFixture.Create<DeleteBookCommand>();
    }
}