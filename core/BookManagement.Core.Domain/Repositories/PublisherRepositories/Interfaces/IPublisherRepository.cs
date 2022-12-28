using System.Linq.Expressions;
using BookManagement.Core.Domain.Entities.Publishers;
using BookManagement.Core.Domain.Queries.PublisherQueries.Inputs;
using BookManagement.Core.Domain.Queries.PublisherQueries.QueryResults;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Repositories.PublisherRepositories.Interfaces;

public interface IPublisherRepository
{
    Task<ICollection<Publisher>> GetAllAsync();
    Task<IPaginateQueryResult<PublisherQueryResult>> GetPaginatedAsync(FilterPublisherQueries filter);
    Task<Publisher> GetByIdAsync(Guid id);
    Task<bool> PublisherExistsAsync(Expression<Func<Publisher, bool>> expression);
    Task<bool> CreateAsync(Publisher publisher);
    Task<bool> UpdateAsync(Publisher publisher);
}