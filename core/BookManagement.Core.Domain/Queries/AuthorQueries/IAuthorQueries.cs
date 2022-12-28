using BookManagement.Core.Domain.Queries.AuthorQueries.Inputs;
using BookManagement.Core.Domain.Queries.AuthorQueries.QueryResults;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Queries.AuthorQueries;

public interface IAuthorQueries
{
    Task<ICollection<AuthorQueryResult>> GetAllAsync();
    Task<AuthorQueryResult> GetByIdAsync(Guid id);
    Task<bool> AuthorExistsAsync(string name);
    Task<IPaginateQueryResult<AuthorQueryResult>> GetPaginatedAsync(FilterAuthorQueries filter);
}