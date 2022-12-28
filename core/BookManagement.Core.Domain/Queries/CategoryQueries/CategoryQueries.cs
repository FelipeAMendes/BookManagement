using AutoMapper;
using BookManagement.Core.Domain.Queries.CategoryQueries.Inputs;
using BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;
using BookManagement.Core.Domain.Repositories.CategoryRepositories.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Queries.CategoryQueries;

public class CategoryQueries : ICategoryQueries
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryQueries(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<ICollection<CategoryQueryResult>> GetAllAsync()
    {
        var category = await _categoryRepository.GetAllAsync();

        return _mapper.Map<ICollection<CategoryQueryResult>>(category);
    }

    public async Task<CategoryQueryResult> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        return _mapper.Map<CategoryQueryResult>(category);
    }

    public async Task<bool> CategoryExistsAsync(Guid id)
    {
        return await _categoryRepository.CategoryExistsAsync(x => x.Id == id);
    }

    public async Task<IPaginateQueryResult<CategoryQueryResult>> GetPaginatedAsync(FilterCategoryQueries filter)
    {
        return await _categoryRepository.GetPaginatedAsync(filter);
    }
}