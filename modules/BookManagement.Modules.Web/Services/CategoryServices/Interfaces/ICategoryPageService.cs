using BookManagement.Core.Domain.Queries.CategoryQueries.Inputs;
using BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Modules.Web.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Services.CategoryServices.Interfaces;

public interface ICategoryPageService
{
    Task<ICollection<CategoryQueryResult>> GetAllAsync();
    Task<CategoryQueryResult> GetByIdAsync(Guid id);
    Task<IPaginateQueryResult<CategoryQueryResult>> GetPaginatedAsync(FilterCategoryQueries filter);
    Task<bool> CategoryExistsAsync(Guid id);
    Task<ICommandResult<CommandNoneResult>> CreateAsync(CategoryModel categoryModel);
    Task<ICommandResult<CommandNoneResult>> UpdateAsync(CategoryModel categoryModel);
    Task<ICommandResult<CommandNoneResult>> DeleteAsync(Guid id);
}