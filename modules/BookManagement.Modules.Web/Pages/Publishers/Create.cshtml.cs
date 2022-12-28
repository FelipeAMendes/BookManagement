using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.Models.PublisherModels;
using BookManagement.Modules.Web.PageFilters;
using BookManagement.Modules.Web.Services.PublisherServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Publishers;

[ServiceFilter(typeof(TransactionPageFilter))]
public class CreateModel : BasePageModel
{
    private readonly IPublisherPageService _publisherPageService;

    public CreateModel(IPublisherPageService publisherPageService)
    {
        _publisherPageService = publisherPageService;
    }

    [BindProperty] public PublisherModel PublisherModel { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        CommandResult = await _publisherPageService.CreateAsync(PublisherModel);

        if (CommandResult.Success)
            return RedirectToPage("./Index");

        return Page();
    }
}