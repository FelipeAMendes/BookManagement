using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.Models.AuthorModels;
using BookManagement.Modules.Web.PageFilters;
using BookManagement.Modules.Web.Services.AuthorServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Authors;

[ServiceFilter(typeof(TransactionPageFilter))]
public class CreateModel : BasePageModel
{
    private readonly IAuthorPageService _authorPageService;

    public CreateModel(IAuthorPageService authorPageService)
    {
        _authorPageService = authorPageService;
    }

    [BindProperty] public AuthorModel AuthorModel { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        CommandResult = await _authorPageService.CreateAsync(AuthorModel);

        if (CommandResult.Success)
            return RedirectToPage("./Index");

        return Page();
    }
}