using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.Models.AuthorModels;
using BookManagement.Modules.Web.PageFilters;
using BookManagement.Modules.Web.Services.AuthorServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Authors;

[ServiceFilter(typeof(TransactionPageFilter))]
public class EditModel : BasePageModel
{
    private readonly IAuthorPageService _authorPageService;

    public EditModel(IAuthorPageService authorPageService)
    {
        _authorPageService = authorPageService;
    }

    [BindProperty] public AuthorModel AuthorModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var author = await _authorPageService.GetByIdAsync(id.Value);
        if (author is null)
            return NotFound();

        AuthorModel = AuthorModel.FromQueryResult(author);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        if (!AuthorModel.Id.HasValue || !await AuthorExists(AuthorModel.Name))
            return NotFound();

        CommandResult = await _authorPageService.UpdateAsync(AuthorModel);

        if (CommandResult.Success)
            return RedirectToPage("./Index");

        return Page();
    }

    private async Task<bool> AuthorExists(string name)
    {
        return await _authorPageService.AuthorExistsAsync(name);
    }
}