using AuthorQueriesInstance = BookManagement.Core.Domain.Queries.AuthorQueries.AuthorQueries;

namespace BookManagement.Core.Domain.UnitTest.Queries.AuthorQueries;

[Collection(nameof(AuthorQueriesTestsCollection))]
public class AuthorQueriesTests
{
    private readonly AuthorQueriesTestsFixture _fixture;
    private readonly AuthorQueriesInstance _authorQueries;

    public AuthorQueriesTests(AuthorQueriesTestsFixture fixture)
    {
        _fixture = fixture;
        _authorQueries = _fixture.GetQueriesInstance();
    }

    [Fact(DisplayName = "Get List of Authors")]
    [Trait("Category", "Author Queries")]
    public async Task ContainsAuthorsInDatabase_GetAllIsCalled_ReturnsAuthorsList()
    {
        _fixture.GetAllRepositoryMock();
        _fixture.GetMappedAuthors();

        var authors = await _authorQueries.GetAllAsync();

        Assert.Equal(5, authors.Count);
    }

    [Fact(DisplayName = "Get Author by Id")]
    [Trait("Category", "Author Queries")]
    public async Task AuthorExists_GetByIdIsCalled_ReturnsAuthor()
    {
        _fixture.GetByIdRepositoryMock();
        _fixture.GetMappedAuthor();

        var book = await _authorQueries.GetByIdAsync(Guid.NewGuid());

        Assert.NotNull(book);
        Assert.NotEmpty(book.Name);
    }

    [Fact(DisplayName = "Get Authors Paginated")]
    [Trait("Category", "Author Queries")]
    public async Task ContainsAuthorsInDatabase_GetPaginatedIsCalled_ReturnsAuthorsPaginated()
    {
        var filter = _fixture.GetPaginationFilter();
        _fixture.GetPaginatedRepositoryMock(filter);

        var paginatedResult = await _authorQueries.GetPaginatedAsync(filter);

        Assert.NotNull(paginatedResult);
        Assert.Equal(expected: 1, paginatedResult.CurrentPage);
        Assert.Equal(expected: 10, paginatedResult.PageSize);
        Assert.Equal(expected: 5, paginatedResult.Pages);
        Assert.Equal(expected: 10, paginatedResult.Items.Count());
    }

    [Fact(DisplayName = "Change")]
    [Trait("Name", "Value")]
    public void Given_When_Then()
    {
        // Arrange

        // Act

        // Assert

    }
}