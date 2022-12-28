using BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;
using BookManagement.Modules.Web.Services.CategoryServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Categories;

public class DetailsModel : PageModel
{
    private readonly ICategoryPageService _categoryPageService;

    public DetailsModel(ICategoryPageService categoryPageService)
    {
        _categoryPageService = categoryPageService;
    }

    public CategoryQueryResult Category { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
            return NotFound();

        var category = await _categoryPageService.GetByIdAsync(id.Value);
        if (category == null)
            return NotFound();

        Category = category;

        return Page();
    }
}