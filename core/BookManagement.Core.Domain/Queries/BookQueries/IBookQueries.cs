using BookManagement.Core.Domain.Queries.BookQueries.Inputs;
using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Queries.BookQueries;

public interface IBookQueries
{
    Task<ICollection<BookQueryResult>> GetAllAsync();
    Task<BookQueryResult> GetByIdAsync(Guid id);
    Task<bool> BookExistsAsync(string isbn10, string isbn13);
    Task<IPaginateQueryResult<BookQueryResult>> GetPaginatedAsync(FilterBookQueries filter);
}