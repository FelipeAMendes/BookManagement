using PublisherQueriesInstance = BookManagement.Core.Domain.Queries.PublisherQueries.PublisherQueries;

namespace BookManagement.Core.Domain.UnitTest.Queries.PublisherQueries;

[Collection(nameof(PublisherQueriesTestsCollection))]
public class PublisherQueriesTests
{
    private readonly PublisherQueriesTestsFixture _fixture;
    private readonly PublisherQueriesInstance _publisherQueries;

    public PublisherQueriesTests(PublisherQueriesTestsFixture fixture)
    {
        _fixture = fixture;
        _publisherQueries = fixture.GetQueriesInstance();
    }

    [Fact(DisplayName = "Get List of Publishers")]
    [Trait("Category", "Publisher Queries")]
    public async Task ContainsPublishersInDatabase_GetAllIsCalled_ReturnsPublishersList()
    {
        _fixture.GetAllRepositoryMock();
        _fixture.GetMappedPublishers();

        var publishers = await _publisherQueries.GetAllAsync();

        Assert.Equal(5, publishers.Count);
    }

    [Fact(DisplayName = "Get Publisher by Id")]
    [Trait("Category", "Publisher Queries")]
    public async Task PublisherExists_GetByIdIsCalled_ReturnsPublisher()
    {
        _fixture.GetByIdRepositoryMock();
        _fixture.GetMappedPublisher();

        var book = await _publisherQueries.GetByIdAsync(Guid.NewGuid());

        Assert.NotNull(book);
        Assert.NotEmpty(book.Name);
    }

    [Fact(DisplayName = "Get Publishers Paginated")]
    [Trait("Category", "Publisher Queries")]
    public async Task ContainsPublishersInDatabase_GetPaginatedIsCalled_ReturnsPublishersPaginated()
    {
        var filter = _fixture.GetPaginationFilter();
        _fixture.GetPaginatedRepositoryMock(filter);

        var paginatedResult = await _publisherQueries.GetPaginatedAsync(filter);

        Assert.NotNull(paginatedResult);
        Assert.Equal(expected: 1, paginatedResult.CurrentPage);
        Assert.Equal(expected: 10, paginatedResult.PageSize);
        Assert.Equal(expected: 5, paginatedResult.Pages);
        Assert.Equal(expected: 10, paginatedResult.Items.Count());
    }
}