using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.PageFilters;
using BookManagement.Modules.Web.Services.BookServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Books;

[ServiceFilter(typeof(TransactionPageFilter))]
public class DeleteModel : BasePageModel
{
    private readonly IBookPageService _bookPageService;

    public DeleteModel(IBookPageService bookPageService)
    {
        _bookPageService = bookPageService;
    }

    [BindProperty] public BookQueryResult Book { get; set; }

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

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var book = await _bookPageService.GetByIdAsync(id.Value);
        if (book is null)
            return NotFound();

        CommandResult = await _bookPageService.DeleteAsync(id.Value);

        return RedirectToPage("./Index");
    }
}