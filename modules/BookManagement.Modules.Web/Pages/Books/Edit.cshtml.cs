using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.Models.BookModels;
using BookManagement.Modules.Web.PageFilters;
using BookManagement.Modules.Web.Services.BookServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Books;

[ServiceFilter(typeof(TransactionPageFilter))]
public class EditModel : BasePageModel
{
    private readonly IBookPageService _bookPageService;

    public EditModel(IBookPageService bookPageService)
    {
        _bookPageService = bookPageService;
    }

    [BindProperty] public BookModel BookModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var book = await _bookPageService.GetByIdAsync(id.Value);
        if (book is null)
            return NotFound();

        BookModel = BookModel.FromQueryResult(book);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        if (!BookModel.Id.HasValue || !await BookExists(BookModel.Isbn10, BookModel.Isbn13))
            return NotFound();

        CommandResult = await _bookPageService.UpdateAsync(BookModel);

        if (CommandResult.Success)
            return RedirectToPage("./Index");

        return Page();
    }

    private async Task<bool> BookExists(string isbn10, string isbn13)
    {
        return await _bookPageService.BookExistsAsync(isbn10, isbn13);
    }
}