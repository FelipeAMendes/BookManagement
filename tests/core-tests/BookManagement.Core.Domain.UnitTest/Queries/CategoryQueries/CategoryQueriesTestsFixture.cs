using AutoFixture;
using AutoMapper;
using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Categories;
using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Core.Domain.Queries.CategoryQueries.Inputs;
using BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;
using BookManagement.Core.Domain.Repositories.CategoryRepositories.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Core.Shared.Queries.QueryResults;
using Moq;
using Moq.AutoMock;
using CategoryQueriesInstance = BookManagement.Core.Domain.Queries.CategoryQueries.CategoryQueries;

namespace BookManagement.Core.Domain.UnitTest.Queries.CategoryQueries;

[CollectionDefinition(nameof(CategoryQueriesTestsCollection))]
public class CategoryQueriesTestsCollection : ICollectionFixture<CategoryQueriesTestsFixture> { }

public class CategoryQueriesTestsFixture
{
    private readonly IFixture _autoFixture;
    private readonly AutoMocker _autoMocker;

    public CategoryQueriesTestsFixture()
    {
        _autoMocker = new AutoMocker();
        _autoFixture = new Fixture();
    }

    public CategoryQueriesInstance GetQueriesInstance()
    {
        return _autoMocker.CreateInstance<CategoryQueriesInstance>();
    }

    public Mock<ICategoryRepository> GetAllRepositoryMock()
    {
        var categoryRepository = _autoMocker.GetMock<ICategoryRepository>();

        categoryRepository
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(_autoFixture.CreateMany<Category>(5).ToList());

        return categoryRepository;
    }

    public Mock<ICategoryRepository> GetByIdRepositoryMock()
    {
        var categoryRepository = _autoMocker.GetMock<ICategoryRepository>();

        categoryRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(_autoFixture.Create<Category>());

        return categoryRepository;
    }

    public Mock<ICategoryRepository> GetPaginatedRepositoryMock(FilterCategoryQueries filter)
    {
        var categoryRepository = _autoMocker.GetMock<ICategoryRepository>();

        categoryRepository
            .Setup(x => x.GetPaginatedAsync(filter))
            .ReturnsAsync(GetPaginatedQueryResult(filter));

        return categoryRepository;
    }

    public FilterCategoryQueries GetPaginationFilter()
    {
        return new FilterCategoryQueries
        {
            CurrentPage = 1,
            PageSize = 10
        };
    }

    public void GetMappedCategories()
    {
        var mapper = _autoMocker.GetMock<IMapper>();

        mapper
            .Setup(x => x.Map<ICollection<CategoryQueryResult>>(It.IsAny<ICollection<Category>>()))
            .Returns(_autoFixture.CreateMany<CategoryQueryResult>(5).ToList());
    }

    public void GetMappedCategory()
    {
        var mapper = _autoMocker.GetMock<IMapper>();

        mapper
            .Setup(x => x.Map<CategoryQueryResult>(It.IsAny<Category>()))
            .Returns(_autoFixture.Create<CategoryQueryResult>());
    }

    private IPaginateQueryResult<CategoryQueryResult> GetPaginatedQueryResult(FilterCategoryQueries filter)
    {
        var items = _autoFixture.CreateMany<CategoryQueryResult>(count: 10).ToList();

        var paginatedCategories = new PaginateQueryResult<CategoryQueryResult>(
            filter.CurrentPage,
            pages: 5,
            filter.PageSize ?? 50,
            total: 50,
            filter.OrderBy,
            items);

        return paginatedCategories;
    }
}