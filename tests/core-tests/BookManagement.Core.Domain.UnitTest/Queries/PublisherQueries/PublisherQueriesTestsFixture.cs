using AutoFixture;
using AutoMapper;
using BookManagement.Core.Domain.Entities.Publishers;
using BookManagement.Core.Domain.Queries.PublisherQueries.Inputs;
using BookManagement.Core.Domain.Queries.PublisherQueries.QueryResults;
using BookManagement.Core.Domain.Repositories.PublisherRepositories.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Core.Shared.Queries.QueryResults;
using Moq;
using Moq.AutoMock;
using PublisherQueriesInstance = BookManagement.Core.Domain.Queries.PublisherQueries.PublisherQueries;

namespace BookManagement.Core.Domain.UnitTest.Queries.PublisherQueries;

[CollectionDefinition(nameof(PublisherQueriesTestsCollection))]
public class PublisherQueriesTestsCollection : ICollectionFixture<PublisherQueriesTestsFixture> { }

public class PublisherQueriesTestsFixture
{
    private readonly IFixture _autoFixture;
    private readonly AutoMocker _autoMocker;

    public PublisherQueriesTestsFixture()
    {
        _autoMocker = new AutoMocker();
        _autoFixture = new Fixture();
    }

    public PublisherQueriesInstance GetQueriesInstance()
    {
        return _autoMocker.CreateInstance<PublisherQueriesInstance>();
    }

    public Mock<IPublisherRepository> GetAllRepositoryMock()
    {
        var publisherRepository = _autoMocker.GetMock<IPublisherRepository>();

        publisherRepository
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(_autoFixture.CreateMany<Publisher>(5).ToList());

        return publisherRepository;
    }

    public Mock<IPublisherRepository> GetByIdRepositoryMock()
    {
        var publisherRepository = _autoMocker.GetMock<IPublisherRepository>();

        publisherRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(_autoFixture.Create<Publisher>());

        return publisherRepository;
    }

    public Mock<IPublisherRepository> GetPaginatedRepositoryMock(FilterPublisherQueries filter)
    {
        var publisherRepository = _autoMocker.GetMock<IPublisherRepository>();

        publisherRepository
            .Setup(x => x.GetPaginatedAsync(filter))
            .ReturnsAsync(GetPaginatedQueryResult(filter));

        return publisherRepository;
    }

    public FilterPublisherQueries GetPaginationFilter()
    {
        return new FilterPublisherQueries
        {
            CurrentPage = 1,
            PageSize = 10
        };
    }

    public void GetMappedPublishers()
    {
        var mapper = _autoMocker.GetMock<IMapper>();

        mapper
            .Setup(x => x.Map<ICollection<PublisherQueryResult>>(It.IsAny<ICollection<Publisher>>()))
            .Returns(_autoFixture.CreateMany<PublisherQueryResult>(5).ToList());
    }

    public void GetMappedPublisher()
    {
        var mapper = _autoMocker.GetMock<IMapper>();

        mapper
            .Setup(x => x.Map<PublisherQueryResult>(It.IsAny<Publisher>()))
            .Returns(_autoFixture.Create<PublisherQueryResult>());
    }

    private IPaginateQueryResult<PublisherQueryResult> GetPaginatedQueryResult(FilterPublisherQueries filter)
    {
        var items = _autoFixture.CreateMany<PublisherQueryResult>(count: 10).ToList();

        var paginatedPublishers = new PaginateQueryResult<PublisherQueryResult>(
            filter.CurrentPage,
            pages: 5,
            filter.PageSize ?? 50,
            total: 50,
            filter.OrderBy,
            items);

        return paginatedPublishers;
    }
}