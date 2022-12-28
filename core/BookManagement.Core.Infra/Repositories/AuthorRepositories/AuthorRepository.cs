using BookManagement.Core.Domain.Entities.Authors;
using BookManagement.Core.Domain.Queries.AuthorQueries.Inputs;
using BookManagement.Core.Domain.Queries.AuthorQueries.QueryResults;
using BookManagement.Core.Domain.Repositories.AuthorRepositories.Interfaces;
using BookManagement.Core.Infra.Data.Interfaces;
using BookManagement.Core.Shared.Extensions.IQueryableExtensions;
using BookManagement.Core.Shared.Extensions.StringExtensions;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using System.Linq.Expressions;

namespace BookManagement.Core.Infra.Repositories.AuthorRepositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly IBookContext _bookContext;

    public AuthorRepository(IBookContext bookContext)
    {
        _bookContext = bookContext;
    }

    public async Task<Author> GetByIdAsync(Guid id)
    {
        return await _bookContext.GetByIdAsync<Author>(id);
    }

    public async Task<ICollection<Author>> GetAllAsync()
    {
        return await _bookContext.WhereAsync<Author>(true, x => true);
    }

    public async Task<bool> AuthorExistsAsync(Expression<Func<Author, bool>> expression)
    {
        return await _bookContext.AnyAsync(expression);
    }

    public async Task<IPaginateQueryResult<AuthorQueryResult>> GetPaginatedAsync(FilterAuthorQueries filter)
    {
        var queryable = _bookContext
            .GetQueryable<Author>(true)
            .WhereIf(filter.Name.HasValue(), x => x.Name.ToLower().Contains(filter.Name.ToLower()));

        return await _bookContext.GetPaginatedProjectionAsync<AuthorQueryResult, Author>(
            filter.CurrentPage,
            filter.PageSize.GetValueOrDefault(),
            filter.OrderBy,
            queryable);
    }

    public async Task<bool> CreateAsync(Author author)
    {
        return await _bookContext.CreateAsync(author);
    }

    public async Task<bool> UpdateAsync(Author author)
    {
        return await _bookContext.UpdateAsync(author);
    }
}