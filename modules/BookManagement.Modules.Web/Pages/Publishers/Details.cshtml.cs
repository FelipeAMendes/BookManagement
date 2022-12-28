using BookManagement.Core.Domain.Queries.PublisherQueries.QueryResults;
using BookManagement.Modules.Web.Services.PublisherServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Publishers;

public class DetailsModel : PageModel
{
    private readonly IPublisherPageService _publisherPageService;

    public DetailsModel(IPublisherPageService publisherPageService)
    {
        _publisherPageService = publisherPageService;
    }

    public PublisherQueryResult Publisher { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
            return NotFound();

        var publisher = await _publisherPageService.GetByIdAsync(id.Value);
        if (publisher is null)
            return NotFound();

        Publisher = publisher;

        return Page();
    }
}