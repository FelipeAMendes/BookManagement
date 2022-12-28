using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Modules.Web.Services.BookServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Books;

public class DetailsModel : PageModel
{
    private readonly IBookPageService _bookPageService;

    public DetailsModel(IBookPageService bookPageService)
    {
        _bookPageService = bookPageService;
    }

    public BookQueryResult Book { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var book = await _bookPageService.GetByIdAsync(id.Value);
        if (book is null)
            return NotFound();

        Book = book;

        return Page();
    }
}