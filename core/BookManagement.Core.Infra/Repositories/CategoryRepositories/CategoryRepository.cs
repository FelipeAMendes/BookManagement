using System.Linq.Expressions;
using BookManagement.Core.Domain.Entities.Categories;
using BookManagement.Core.Domain.Queries.CategoryQueries.Inputs;
using BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;
using BookManagement.Core.Domain.Repositories.CategoryRepositories.Interfaces;
using BookManagement.Core.Infra.Data.Interfaces;
using BookManagement.Core.Shared.Extensions.IQueryableExtensions;
using BookManagement.Core.Shared.Extensions.StringExtensions;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Core.Infra.Repositories.CategoryRepositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IBookContext _bookContext;

    public CategoryRepository(IBookContext bookContext)
    {
        _bookContext = bookContext;
    }

    public async Task<Category> GetByIdAsync(Guid id)
    {
        return await _bookContext.GetByIdAsync<Category>(id, x => x.ParentCategory);
    }

    public async Task<ICollection<Category>> GetAllAsync()
    {
        var queryable = _bookContext
            .GetQueryable<Category>(true)
            .OrderBy(x => x.Name);

        return await queryable.ToListAsync();
    }

    public async Task<bool> CategoryExistsAsync(Expression<Func<Category, bool>> expression)
    {
        return await _bookContext.AnyAsync(expression);
    }

    public async Task<IPaginateQueryResult<CategoryQueryResult>> GetPaginatedAsync(FilterCategoryQueries filter)
    {
        var queryable = _bookContext
            .GetQueryable<Category>(true)
            .WhereIf(filter.Name.HasValue(), x => x.Name.ToLower().Contains(filter.Name.ToLower()));

        return await _bookContext.GetPaginatedProjectionAsync<CategoryQueryResult, Category>(
            filter.CurrentPage,
            filter.PageSize.GetValueOrDefault(),
            filter.OrderBy,
            queryable);
    }

    public async Task<bool> CreateAsync(Category category)
    {
        return await _bookContext.CreateAsync(category);
    }

    public async Task<bool> UpdateAsync(Category category)
    {
        return await _bookContext.UpdateAsync(category);
    }
}