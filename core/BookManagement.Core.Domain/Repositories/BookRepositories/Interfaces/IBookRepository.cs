using System.Linq.Expressions;
using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Queries.BookQueries.Inputs;
using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Repositories.BookRepositories.Interfaces;

public interface IBookRepository
{
    Task<ICollection<Book>> GetAllAsync(bool includeProperties);
    Task<Book> GetByIdAsync(Guid id);
    Task<bool> BookExistsAsync(Expression<Func<Book, bool>> expression);
    Task<IPaginateQueryResult<BookQueryResult>> GetPaginatedAsync(FilterBookQueries filter);
    Task<bool> CreateAsync(Book book);
    Task<bool> UpdateAsync(Book book);
}