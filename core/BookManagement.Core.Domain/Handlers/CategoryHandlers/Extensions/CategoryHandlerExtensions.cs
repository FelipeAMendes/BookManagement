using BookManagement.Core.Domain.Entities.Categories;
using BookManagement.Core.Domain.Repositories.CategoryRepositories.Interfaces;
using BookManagement.Core.Shared.Extensions.EnumExtensions;
using BookManagement.Core.Shared.Extensions.ObjectExtensions;
using BookManagement.Core.Shared.Handlers;

namespace BookManagement.Core.Domain.Handlers.CategoryHandlers.Extensions;

public static class CategoryHandlerExtensions
{
    public static async Task SetParentCategory(this CategoryHandler categoryHandler,
        ICategoryRepository categoryRepository, Category category, Guid? parentCategoryId)
    {
        if (!parentCategoryId.HasValue)
            return;

        var sameParentCategory = category.ParentCategory?.Id == parentCategoryId.Value;
        if (sameParentCategory)
            return;

        var parentCategory = await categoryRepository.GetByIdAsync(parentCategoryId.Value);
        if (parentCategory.IsNull())
        {
            categoryHandler.AddError(nameof(parentCategoryId), ValidationType.ItemNotFound.GetDescription());
            return;
        }

        category.SetParent(parentCategory);
    }
}