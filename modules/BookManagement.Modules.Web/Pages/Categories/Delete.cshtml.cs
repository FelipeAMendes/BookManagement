using BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;
using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.PageFilters;
using BookManagement.Modules.Web.Services.CategoryServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Categories;

[ServiceFilter(typeof(TransactionPageFilter))]
public class DeleteModel : BasePageModel
{
    private readonly ICategoryPageService _categoryPageService;

    public DeleteModel(ICategoryPageService categoryPageService)
    {
        _categoryPageService = categoryPageService;
    }

    [BindProperty] public CategoryQueryResult Category { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var category = await _categoryPageService.GetByIdAsync(id.Value);

        if (category is null)
            return NotFound();

        Category = category;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var category = await _categoryPageService.GetByIdAsync(id.Value);

        if (category is null)
            return NotFound();

        CommandResult = await _categoryPageService.DeleteAsync(id.Value);

        return RedirectToPage("./Index");
    }
}