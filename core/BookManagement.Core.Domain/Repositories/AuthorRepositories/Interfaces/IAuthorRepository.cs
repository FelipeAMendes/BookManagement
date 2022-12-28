using BookManagement.Core.Domain.Entities.Authors;
using BookManagement.Core.Domain.Queries.AuthorQueries.Inputs;
using BookManagement.Core.Domain.Queries.AuthorQueries.QueryResults;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using System.Linq.Expressions;

namespace BookManagement.Core.Domain.Repositories.AuthorRepositories.Interfaces;

public interface IAuthorRepository
{
    Task<ICollection<Author>> GetAllAsync();
    Task<Author> GetByIdAsync(Guid id);
    Task<bool> AuthorExistsAsync(Expression<Func<Author, bool>> expression);
    Task<IPaginateQueryResult<AuthorQueryResult>> GetPaginatedAsync(FilterAuthorQueries filter);
    Task<bool> CreateAsync(Author author);
    Task<bool> UpdateAsync(Author author);
}