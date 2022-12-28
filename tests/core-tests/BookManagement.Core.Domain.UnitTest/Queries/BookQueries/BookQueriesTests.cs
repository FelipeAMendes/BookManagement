using BookQueriesInstance = BookManagement.Core.Domain.Queries.BookQueries.BookQueries;

namespace BookManagement.Core.Domain.UnitTest.Queries.BookQueries;

[Collection(nameof(BookQueriesTestsCollection))]
public class BookQueriesTests
{
    private readonly BookQueriesTestsFixture _fixture;
    private readonly BookQueriesInstance _bookQueries;

    public BookQueriesTests(BookQueriesTestsFixture fixture)
    {
        _fixture = fixture;
        _bookQueries = fixture.GetQueriesInstance();
    }

    [Fact(DisplayName = "Get List of Books")]
    [Trait("Category", "Book Queries")]
    public async Task ContainsBooksInDatabase_GetAllIsCalled_ReturnsBooksList()
    {
        _fixture.GetAllRepositoryMock();
        _fixture.GetMappedBooks();

        var books = await _bookQueries.GetAllAsync();

        Assert.Equal(5, books.Count);
    }

    [Fact(DisplayName = "Get Book by Id")]
    [Trait("Category", "Book Queries")]
    public async Task BookExists_GetByIdIsCalled_ReturnsBook()
    {
        _fixture.GetByIdRepositoryMock();
        _fixture.GetMappedBook();

        var book = await _bookQueries.GetByIdAsync(Guid.NewGuid());

        Assert.NotNull(book);
        Assert.NotEmpty(book.Title);
    }

    [Fact(DisplayName = "Get Books Paginated")]
    [Trait("Category", "Book Queries")]
    public async Task ContainsBooksInDatabase_GetPaginatedIsCalled_ReturnsBooksPaginated()
    {
        var filter = _fixture.GetPaginationFilter();
        _fixture.GetPaginatedRepositoryMock(filter);

        var paginatedResult = await _bookQueries.GetPaginatedAsync(filter);

        Assert.NotNull(paginatedResult);
        Assert.Equal(expected: 1, paginatedResult.CurrentPage);
        Assert.Equal(expected: 10, paginatedResult.PageSize);
        Assert.Equal(expected: 5, paginatedResult.Pages);
        Assert.Equal(expected: 10, paginatedResult.Items.Count());
    }
}