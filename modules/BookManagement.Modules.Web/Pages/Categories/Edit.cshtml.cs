using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.Models.CategoryModels;
using BookManagement.Modules.Web.PageFilters;
using BookManagement.Modules.Web.Services.CategoryServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Categories;

[ServiceFilter(typeof(TransactionPageFilter))]
public class EditModel : BasePageModel
{
    private readonly ICategoryPageService _categoryPageService;

    public EditModel(ICategoryPageService categoryPageService)
    {
        _categoryPageService = categoryPageService;
    }

    [BindProperty] public CategoryModel CategoryModel { get; set; } = default!;

    public SelectList CategoriesSelectList { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var category = await _categoryPageService.GetByIdAsync(id.Value);
        if (category is null)
            return NotFound();

        CategoryModel = CategoryModel.FromQueryResult(category);
        await LoadParentCategories();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await LoadParentCategories();

        if (!ModelState.IsValid)
            return Page();

        if (!CategoryModel.Id.HasValue || !await CategoryExists(CategoryModel.Id.Value))
            return NotFound();

        CommandResult = await _categoryPageService.UpdateAsync(CategoryModel);

        if (CommandResult.Success)
            return RedirectToPage("./Index");

        return Page();
    }

    private async Task<bool> CategoryExists(Guid id)
    {
        return await _categoryPageService.CategoryExistsAsync(id);
    }

    public async Task LoadParentCategories()
    {
        var categories = await _categoryPageService.GetAllAsync();
        CategoriesSelectList = CreateSelectList(categories.Select(x => new SelectListModel(x.Id, x.Name)), "Id", "Name", CategoryModel?.ParentCategoryId);
    }
}