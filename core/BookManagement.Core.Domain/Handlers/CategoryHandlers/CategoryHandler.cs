using BookManagement.Core.Domain.Commands.CategoryCommands;
using BookManagement.Core.Domain.Handlers.CategoryHandlers.Extensions;
using BookManagement.Core.Domain.Handlers.CategoryHandlers.Interfaces;
using BookManagement.Core.Domain.Repositories.CategoryRepositories.Interfaces;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Handlers;

namespace BookManagement.Core.Domain.Handlers.CategoryHandlers;

public class CategoryHandler : BaseHandler, ICategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ICommandResult<CommandNoneResult>> HandleAsync(CreateCategoryCommand createCommand)
    {
        var category = createCommand.ToEntity();

        await this.SetParentCategory(_categoryRepository, category, createCommand.ParentCategoryId);

        AddErrors(category.Errors);

        if (!IsValid)
            return CreateErrorCommandResult(Errors);

        var categoryCreated = await _categoryRepository.CreateAsync(category);
        return CreateDefaultCommandResult(categoryCreated, ValidationType.CreationError);
    }

    public async Task<ICommandResult<CommandNoneResult>> HandleAsync(UpdateCategoryCommand updateCommand)
    {
        var category = await _categoryRepository.GetByIdAsync(updateCommand.Id);

        if (category is null)
            return CreateNotFoundCommandResult();

        updateCommand.UpdateEntity(category);

        await this.SetParentCategory(_categoryRepository, category, updateCommand.ParentCategoryId);

        AddErrors(category.Errors);

        if (!IsValid)
            return CreateErrorCommandResult(Errors);

        var categoryUpdated = await _categoryRepository.UpdateAsync(category);
        return CreateDefaultCommandResult(categoryUpdated, ValidationType.ChangeError);
    }

    public async Task<ICommandResult<CommandNoneResult>> HandleAsync(DeleteCategoryCommand deleteCommand)
    {
        var category = await _categoryRepository.GetByIdAsync(deleteCommand.Id);

        if (category is null)
            return CreateNotFoundCommandResult();

        category.Remove();

        var categoryUpdated = await _categoryRepository.UpdateAsync(category);
        return CreateDefaultCommandResult(categoryUpdated, ValidationType.RemovalError);
    }
}