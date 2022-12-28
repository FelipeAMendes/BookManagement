using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.Models.BookModels;
using BookManagement.Modules.Web.PageFilters;
using BookManagement.Modules.Web.Services.BookServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Books;

[ServiceFilter(typeof(TransactionPageFilter))]
public class CreateModel : BasePageModel
{
    private readonly IBookPageService _bookPageService;

    public CreateModel(IBookPageService bookPageService)
    {
        _bookPageService = bookPageService;
    }

    [BindProperty] public BookModel BookModel { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        CommandResult = await _bookPageService.CreateAsync(BookModel);

        if (CommandResult.Success)
            return RedirectToPage("./Index");

        return Page();
    }
}