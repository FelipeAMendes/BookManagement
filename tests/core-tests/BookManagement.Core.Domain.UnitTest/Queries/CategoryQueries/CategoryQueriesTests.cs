using CategoryQueriesInstance = BookManagement.Core.Domain.Queries.CategoryQueries.CategoryQueries;

namespace BookManagement.Core.Domain.UnitTest.Queries.CategoryQueries;

[Collection(nameof(CategoryQueriesTestsCollection))]
public class CategoryQueriesTests
{
    private readonly CategoryQueriesTestsFixture _fixture;
    private readonly CategoryQueriesInstance _categoryQueries;

    public CategoryQueriesTests(CategoryQueriesTestsFixture fixture)
    {
        _fixture = fixture;
        _categoryQueries = fixture.GetQueriesInstance();
    }

    [Fact(DisplayName = "Get List of Categories")]
    [Trait("Category", "Category Queries")]
    public async Task ContainsCategoriesInDatabase_GetAllIsCalled_ReturnsCategoriesList()
    {
        _fixture.GetAllRepositoryMock();
        _fixture.GetMappedCategories();

        var books = await _categoryQueries.GetAllAsync();

        Assert.Equal(5, books.Count);
    }

    [Fact(DisplayName = "Get Category by Id")]
    [Trait("Category", "Category Queries")]
    public async Task CategoryExists_GetByIdIsCalled_ReturnsCategory()
    {
        _fixture.GetByIdRepositoryMock();
        _fixture.GetMappedCategory();

        var book = await _categoryQueries.GetByIdAsync(Guid.NewGuid());

        Assert.NotNull(book);
        Assert.NotEmpty(book.Name);
    }

    [Fact(DisplayName = "Get Categories Paginated")]
    [Trait("Category", "Category Queries")]
    public async Task ContainsCategoriesInDatabase_GetPaginatedIsCalled_ReturnsCategoriesPaginated()
    {
        var filter = _fixture.GetPaginationFilter();
        _fixture.GetPaginatedRepositoryMock(filter);

        var paginatedResult = await _categoryQueries.GetPaginatedAsync(filter);

        Assert.NotNull(paginatedResult);
        Assert.Equal(expected: 1, paginatedResult.CurrentPage);
        Assert.Equal(expected: 10, paginatedResult.PageSize);
        Assert.Equal(expected: 5, paginatedResult.Pages);
        Assert.Equal(expected: 10, paginatedResult.Items.Count());
    }
}