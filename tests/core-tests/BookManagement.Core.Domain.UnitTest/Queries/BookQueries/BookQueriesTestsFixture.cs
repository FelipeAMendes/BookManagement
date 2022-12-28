using AutoFixture;
using AutoMapper;
using Bogus;
using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using BookManagement.Core.Domain.Queries.BookQueries.Inputs;
using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Core.Domain.Repositories.BookRepositories.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Core.Shared.Queries.QueryResults;
using Moq;
using Moq.AutoMock;
using BookQueriesInstance = BookManagement.Core.Domain.Queries.BookQueries.BookQueries;

namespace BookManagement.Core.Domain.UnitTest.Queries.BookQueries;

[CollectionDefinition(nameof(BookQueriesTestsCollection))]
public class BookQueriesTestsCollection : ICollectionFixture<BookQueriesTestsFixture> { }

public class BookQueriesTestsFixture
{
    private readonly IFixture _autoFixture;
    private readonly AutoMocker _autoMocker;

    public BookQueriesTestsFixture()
    {
        _autoMocker = new AutoMocker();
        _autoFixture = new Fixture();
    }

    public BookQueriesInstance GetQueriesInstance()
    {
        return _autoMocker.CreateInstance<BookQueriesInstance>();
    }

    public Mock<IBookRepository> GetAllRepositoryMock()
    {
        var bookRepository = _autoMocker.GetMock<IBookRepository>();

        bookRepository
            .Setup(x => x.GetAllAsync(false))
            .ReturnsAsync(GetBooks(5, 5));

        return bookRepository;
    }

    public Mock<IBookRepository> GetByIdRepositoryMock()
    {
        var bookRepository = _autoMocker.GetMock<IBookRepository>();

        bookRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(GetBooks(1, 1).First);

        return bookRepository;
    }

    public Mock<IBookRepository> GetPaginatedRepositoryMock(FilterBookQueries filter)
    {
        var bookRepository = _autoMocker.GetMock<IBookRepository>();

        bookRepository
            .Setup(x => x.GetPaginatedAsync(filter))
            .ReturnsAsync(GetPaginatedQueryResult(filter));

        return bookRepository;
    }

    public FilterBookQueries GetPaginationFilter()
    {
        return new FilterBookQueries
        {
            CurrentPage = 1,
            PageSize = 10
        };
    }

    public void GetMappedBooks()
    {
        var mapper = _autoMocker.GetMock<IMapper>();

        mapper
            .Setup(x => x.Map<ICollection<BookQueryResult>>(It.IsAny<ICollection<Book>>()))
            .Returns(GetBooksQueryResult(5, 5));
    }

    public void GetMappedBook()
    {
        var mapper = _autoMocker.GetMock<IMapper>();

        mapper
            .Setup(x => x.Map<BookQueryResult>(It.IsAny<Book>()))
            .Returns(GetBooksQueryResult(1, 1).First);
    }

    public List<Book> GetBooks(int min, int max)
    {
        var book = new Faker<Book>()
            .CustomInstantiator(x =>
            {
                var title = x.Lorem.Text();
                var description = x.Lorem.Text();
                var format = x.Random.Enum<Format>();

                return new Book(title, description, DateTime.Now, format, null, null);
            });

        return book.GenerateBetween(min, max);
    }

    public List<BookQueryResult> GetBooksQueryResult(int min, int max)
    {
        var book = new Faker<BookQueryResult>()
            .CustomInstantiator(x =>
            {
                var title = x.Random.String2(BookSpecification.TitleColumnSize);
                var description = x.Random.String(1, BookSpecification.DescriptionColumnSize);
                var format = x.Random.Enum<Format>();

                return new BookQueryResult
                {
                    Title = title,
                    Description = description,
                    Format = format
                };
            });

        return book.GenerateBetween(min, max);
    }

    private IPaginateQueryResult<BookQueryResult> GetPaginatedQueryResult(FilterBookQueries filter)
    {
        var items = _autoFixture.CreateMany<BookQueryResult>(count: 10).ToList();

        var paginatedBooks = new PaginateQueryResult<BookQueryResult>(
            filter.CurrentPage,
            pages: 5,
            filter.PageSize ?? 50,
            total: 50,
            filter.OrderBy,
            items);

        return paginatedBooks;
    }
}