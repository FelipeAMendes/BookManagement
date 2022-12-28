using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Queries.BookQueries.Inputs;
using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Core.Domain.Repositories.BookRepositories.Interfaces;
using BookManagement.Core.Infra.Data.Interfaces;
using BookManagement.Core.Shared.Extensions.IQueryableExtensions;
using BookManagement.Core.Shared.Extensions.StringExtensions;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookManagement.Core.Infra.Repositories.BookRepositories;

public class BookRepository : IBookRepository
{
    private readonly IBookContext _bookContext;

    public BookRepository(IBookContext bookContext)
    {
        _bookContext = bookContext;
    }

    public async Task<Book> GetByIdAsync(Guid id)
    {
        var includes = new Expression<Func<Book, object>>[]
        {
            x => x.Keywords,
            x => x.Quotes,
            x => x.Reviews
        };

        return await _bookContext.GetByIdAsync(id, includes);
    }

    public async Task<ICollection<Book>> GetAllAsync(bool includeProperties)
    {
        if (!includeProperties)
            return await _bookContext.AllAsync<Book>();

        var includes = new Expression<Func<Book, object>>[]
        {
            x => x.Author,
            x => x.Category,
            x => x.Publisher
        };

        return await _bookContext
            .GetQueryable(true, includes)
            .ToListAsync();
    }

    public async Task<IPaginateQueryResult<BookQueryResult>> GetPaginatedAsync(FilterBookQueries filter)
    {
        var queryable = _bookContext
            .GetQueryable<Book>(true)
            .WhereIf(filter.Title.HasValue(), x => x.Title.ToLower().Contains(filter.Title.ToLower()));

        return await _bookContext.GetPaginatedProjectionAsync<BookQueryResult, Book>(
            filter.CurrentPage,
            filter.PageSize.GetValueOrDefault(),
            filter.OrderBy,
            queryable);
    }

    public async Task<bool> BookExistsAsync(Expression<Func<Book, bool>> expression)
    {
        return await _bookContext.AnyAsync(expression);
    }

    public async Task<bool> CreateAsync(Book book)
    {
        return await _bookContext.CreateAsync(book);
    }

    public async Task<bool> UpdateAsync(Book book)
    {
        return await _bookContext.UpdateAsync(book);
    }
}