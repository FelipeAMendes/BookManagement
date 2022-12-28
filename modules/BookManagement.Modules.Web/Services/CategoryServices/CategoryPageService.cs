using BookManagement.Core.Domain.Commands.CategoryCommands;
using BookManagement.Core.Domain.Handlers.CategoryHandlers.Interfaces;
using BookManagement.Core.Domain.Queries.CategoryQueries;
using BookManagement.Core.Domain.Queries.CategoryQueries.Inputs;
using BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Modules.Web.Models.CategoryModels;
using BookManagement.Modules.Web.Services.CategoryServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Services.CategoryServices;

public class CategoryPageService : ICategoryPageService
{
    private readonly ICategoryHandler _categoryHandler;
    private readonly ICategoryQueries _categoryQueries;

    public CategoryPageService(ICategoryHandler categoryHandler, ICategoryQueries categoryQueries)
    {
        _categoryHandler = categoryHandler;
        _categoryQueries = categoryQueries;
    }

    public async Task<ICollection<CategoryQueryResult>> GetAllAsync()
    {
        var categories = await _categoryQueries.GetAllAsync();

        return categories;
    }

    public async Task<CategoryQueryResult> GetByIdAsync(Guid id)
    {
        var category = await _categoryQueries.GetByIdAsync(id);

        return category;
    }

    public async Task<IPaginateQueryResult<CategoryQueryResult>> GetPaginatedAsync(FilterCategoryQueries filter)
    {
        var publishers = await _categoryQueries.GetPaginatedAsync(filter);

        return publishers;
    }

    public async Task<bool> CategoryExistsAsync(Guid id)
    {
        return await _categoryQueries.CategoryExistsAsync(id);
    }

    public async Task<ICommandResult<CommandNoneResult>> CreateAsync(CategoryModel categoryModel)
    {
        var createCommand = new CreateCategoryCommand
        {
            Name = categoryModel.Name,
            Description = categoryModel.Description,
            ParentCategoryId = categoryModel.ParentCategoryId
        };

        var commandResult = await _categoryHandler.HandleAsync(createCommand);

        return commandResult;
    }

    public async Task<ICommandResult<CommandNoneResult>> UpdateAsync(CategoryModel categoryModel)
    {
        var updateCommand = new UpdateCategoryCommand
        {
            Id = categoryModel.Id ?? Guid.Empty,
            Name = categoryModel.Name,
            Description = categoryModel.Description,
            ParentCategoryId = categoryModel.ParentCategoryId
        };

        var commandResult = await _categoryHandler.HandleAsync(updateCommand);

        return commandResult;
    }

    public async Task<ICommandResult<CommandNoneResult>> DeleteAsync(Guid id)
    {
        var deleteCommand = new DeleteCategoryCommand(id);

        var commandResult = await _categoryHandler.HandleAsync(deleteCommand);

        return commandResult;
    }
}