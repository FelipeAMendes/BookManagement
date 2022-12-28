using AutoFixture;
using AutoMapper;
using BookManagement.Core.Domain.Entities.Authors;
using BookManagement.Core.Domain.Queries.AuthorQueries.Inputs;
using BookManagement.Core.Domain.Queries.AuthorQueries.QueryResults;
using BookManagement.Core.Domain.Repositories.AuthorRepositories.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Core.Shared.Queries.QueryResults;
using Moq;
using Moq.AutoMock;
using AuthorQueriesInstance = BookManagement.Core.Domain.Queries.AuthorQueries.AuthorQueries;

namespace BookManagement.Core.Domain.UnitTest.Queries.AuthorQueries;

[CollectionDefinition(nameof(AuthorQueriesTestsCollection))]
public class AuthorQueriesTestsCollection : ICollectionFixture<AuthorQueriesTestsFixture> { }

public class AuthorQueriesTestsFixture
{
    private readonly IFixture _autoFixture;
    private readonly AutoMocker _autoMocker;

    public AuthorQueriesTestsFixture()
    {
        _autoMocker = new AutoMocker();
        _autoFixture = new Fixture();
    }

    public AuthorQueriesInstance GetQueriesInstance()
    {
        return _autoMocker.CreateInstance<AuthorQueriesInstance>();
    }

    public Mock<IAuthorRepository> GetAllRepositoryMock()
    {
        var authorRepository = _autoMocker.GetMock<IAuthorRepository>();

        authorRepository
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(_autoFixture.CreateMany<Author>(5).ToList());

        return authorRepository;
    }

    public Mock<IAuthorRepository> GetByIdRepositoryMock()
    {
        var authorRepository = _autoMocker.GetMock<IAuthorRepository>();

        authorRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(_autoFixture.Create<Author>());

        return authorRepository;
    }

    public Mock<IAuthorRepository> GetPaginatedRepositoryMock(FilterAuthorQueries filter)
    {
        var authorRepository = _autoMocker.GetMock<IAuthorRepository>();

        authorRepository
            .Setup(x => x.GetPaginatedAsync(filter))
            .ReturnsAsync(GetPaginatedQueryResult(filter));

        return authorRepository;
    }

    public void GetMappedAuthors()
    {
        var mapper = _autoMocker.GetMock<IMapper>();

        mapper
            .Setup(x => x.Map<ICollection<AuthorQueryResult>>(It.IsAny<ICollection<Author>>()))
            .Returns(_autoFixture.CreateMany<AuthorQueryResult>(5).ToList());
    }

    public void GetMappedAuthor()
    {
        var mapper = _autoMocker.GetMock<IMapper>();

        mapper
            .Setup(x => x.Map<AuthorQueryResult>(It.IsAny<Author>()))
            .Returns(_autoFixture.Create<AuthorQueryResult>());
    }

    public FilterAuthorQueries GetPaginationFilter()
    {
        return new FilterAuthorQueries
        {
            CurrentPage = 1,
            PageSize = 10
        };
    }

    private IPaginateQueryResult<AuthorQueryResult> GetPaginatedQueryResult(FilterAuthorQueries filter)
    {
        var items = _autoFixture.CreateMany<AuthorQueryResult>(count: 10).ToList();

        var paginatedAuthors = new PaginateQueryResult<AuthorQueryResult>(
            filter.CurrentPage,
            pages: 5,
            filter.PageSize ?? 50,
            total: 50,
            filter.OrderBy,
            items);

        return paginatedAuthors;
    }
}