using System.Linq.Expressions;
using BookManagement.Core.Domain.Entities.Publishers;
using BookManagement.Core.Domain.Queries.PublisherQueries.Inputs;
using BookManagement.Core.Domain.Queries.PublisherQueries.QueryResults;
using BookManagement.Core.Domain.Repositories.PublisherRepositories.Interfaces;
using BookManagement.Core.Infra.Data.Interfaces;
using BookManagement.Core.Shared.Extensions.IQueryableExtensions;
using BookManagement.Core.Shared.Extensions.StringExtensions;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Infra.Repositories.PublisherRepositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly IBookContext _bookContext;

    public PublisherRepository(IBookContext bookContext)
    {
        _bookContext = bookContext;
    }

    public async Task<Publisher> GetByIdAsync(Guid id)
    {
        return await _bookContext.GetByIdAsync<Publisher>(id);
    }

    public async Task<ICollection<Publisher>> GetAllAsync()
    {
        return await _bookContext.WhereAsync<Publisher>(true, x => true);
    }

    public async Task<IPaginateQueryResult<PublisherQueryResult>> GetPaginatedAsync(FilterPublisherQueries filter)
    {
        var queryable = _bookContext
            .GetQueryable<Publisher>(true)
            .WhereIf(filter.Name.HasValue(), x => x.Name.Contains(filter.Name));

        return await _bookContext.GetPaginatedProjectionAsync<PublisherQueryResult, Publisher>(
            filter.CurrentPage,
            filter.PageSize.GetValueOrDefault(),
            filter.OrderBy,
            queryable);
    }

    public async Task<bool> PublisherExistsAsync(Expression<Func<Publisher, bool>> expression)
    {
        return await _bookContext.AnyAsync(expression);
    }

    public async Task<bool> CreateAsync(Publisher publisher)
    {
        return await _bookContext.CreateAsync(publisher);
    }

    public async Task<bool> UpdateAsync(Publisher publisher)
    {
        return await _bookContext.UpdateAsync(publisher);
    }
}