using System.Linq.Expressions;
using BookManagement.Core.Domain.Entities.Categories;
using BookManagement.Core.Domain.Queries.CategoryQueries.Inputs;
using BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Repositories.CategoryRepositories.Interfaces;

public interface ICategoryRepository
{
    Task<ICollection<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(Guid id);
    Task<bool> CategoryExistsAsync(Expression<Func<Category, bool>> expression);
    Task<IPaginateQueryResult<CategoryQueryResult>> GetPaginatedAsync(FilterCategoryQueries filter);
    Task<bool> CreateAsync(Category category);
    Task<bool> UpdateAsync(Category category);
}