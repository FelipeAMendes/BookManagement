using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.Models.CategoryModels;
using BookManagement.Modules.Web.PageFilters;
using BookManagement.Modules.Web.Services.CategoryServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Categories;

[ServiceFilter(typeof(TransactionPageFilter))]
public class CreateModel : BasePageModel
{
    private readonly ICategoryPageService _categoryPageService;

    public CreateModel(ICategoryPageService categoryPageService)
    {
        _categoryPageService = categoryPageService;
    }

    [BindProperty] public CategoryModel CategoryModel { get; set; }

    public SelectList CategoriesSelectList { get; set; }

    public async Task<IActionResult> OnGet()
    {
        await LoadParentCategories();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await LoadParentCategories();

        if (!ModelState.IsValid)
            return Page();

        CommandResult = await _categoryPageService.CreateAsync(CategoryModel);

        if (CommandResult.Success)
            return RedirectToPage("./Index");

        return Page();
    }

    public async Task LoadParentCategories()
    {
        var categories = await _categoryPageService.GetAllAsync();
        CategoriesSelectList = CreateSelectList(categories.Select(x => new SelectListModel(x.Id, x.Name)), "Id", "Name");
    }
}