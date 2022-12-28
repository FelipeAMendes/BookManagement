using BookManagement.Core.Domain.Queries.CategoryQueries.Inputs;
using BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Queries.CategoryQueries;

public interface ICategoryQueries
{
    Task<ICollection<CategoryQueryResult>> GetAllAsync();
    Task<CategoryQueryResult> GetByIdAsync(Guid id);
    Task<bool> CategoryExistsAsync(Guid id);
    Task<IPaginateQueryResult<CategoryQueryResult>> GetPaginatedAsync(FilterCategoryQueries filter);
}