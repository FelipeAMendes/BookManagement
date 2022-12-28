using BookManagement.Core.Domain.Queries.PublisherQueries.QueryResults;
using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.PageFilters;
using BookManagement.Modules.Web.Services.PublisherServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Publishers;

[ServiceFilter(typeof(TransactionPageFilter))]
public class DeleteModel : BasePageModel
{
    private readonly IPublisherPageService _publisherPageService;

    public DeleteModel(IPublisherPageService publisherPageService)
    {
        _publisherPageService = publisherPageService;
    }

    [BindProperty] public PublisherQueryResult Publisher { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var publisher = await _publisherPageService.GetByIdAsync(id.Value);
        if (publisher is null)
            return NotFound();

        Publisher = publisher;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var publisher = await _publisherPageService.GetByIdAsync(id.Value);
        if (publisher is null)
            return NotFound();

        CommandResult = await _publisherPageService.DeleteAsync(id.Value);

        return RedirectToPage("./Index");
    }
}