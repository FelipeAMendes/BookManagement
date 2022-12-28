using BookManagement.Core.Domain.Queries.PublisherQueries.Inputs;
using BookManagement.Core.Domain.Queries.PublisherQueries.QueryResults;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Queries.PublisherQueries;

public interface IPublisherQueries
{
    Task<ICollection<PublisherQueryResult>> GetAllAsync();
    Task<PublisherQueryResult> GetByIdAsync(Guid id);
    Task<bool> PublisherExistsAsync(string name);
    Task<IPaginateQueryResult<PublisherQueryResult>> GetPaginatedAsync(FilterPublisherQueries filter);
}