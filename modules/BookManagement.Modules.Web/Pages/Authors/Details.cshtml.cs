using BookManagement.Core.Domain.Queries.AuthorQueries.QueryResults;
using BookManagement.Modules.Web.Services.AuthorServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Authors;

public class DetailsModel : PageModel
{
    private readonly IAuthorPageService _authorPageService;

    public DetailsModel(IAuthorPageService authorPageService)
    {
        _authorPageService = authorPageService;
    }

    public AuthorQueryResult Author { get; set; }

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
}