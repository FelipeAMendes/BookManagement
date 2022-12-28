using BookManagement.Core.Domain.Queries.AuthorQueries.QueryResults;
using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.PageFilters;
using BookManagement.Modules.Web.Services.AuthorServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Authors;

[ServiceFilter(typeof(TransactionPageFilter))]
public class DeleteModel : BasePageModel
{
    private readonly IAuthorPageService _authorPageService;

    public DeleteModel(IAuthorPageService authorPageService)
    {
        _authorPageService = authorPageService;
    }

    [BindProperty] public AuthorQueryResult Author { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var author = await _authorPageService.GetByIdAsync(id.Value);
        if (author is null)
            return NotFound();

        Author = author;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var author = await _authorPageService.GetByIdAsync(id.Value);
        if (author is null)
            return NotFound();

        CommandResult = await _authorPageService.DeleteAsync(id.Value);

        return RedirectToPage("./Index");
    }
}