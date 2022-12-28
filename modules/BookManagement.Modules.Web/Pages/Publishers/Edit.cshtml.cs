using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.Models.PublisherModels;
using BookManagement.Modules.Web.PageFilters;
using BookManagement.Modules.Web.Services.PublisherServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Publishers;

[ServiceFilter(typeof(TransactionPageFilter))]
public class EditModel : BasePageModel
{
    private readonly IPublisherPageService _publisherPageService;

    public EditModel(IPublisherPageService publisherPageService)
    {
        _publisherPageService = publisherPageService;
    }

    [BindProperty] public PublisherModel PublisherModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var publisher = await _publisherPageService.GetByIdAsync(id.Value);
        if (publisher is null)
            return NotFound();

        PublisherModel = PublisherModel.FromQueryResult(publisher);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        if (!PublisherModel.Id.HasValue || !await PublisherExists(PublisherModel.Name))
            return NotFound();

        CommandResult = await _publisherPageService.UpdateAsync(PublisherModel);

        if (CommandResult.Success)
            return RedirectToPage("./Index");

        return Page();
    }

    private async Task<bool> PublisherExists(string name)
    {
        return await _publisherPageService.PublisherExistsAsync(name);
    }
}