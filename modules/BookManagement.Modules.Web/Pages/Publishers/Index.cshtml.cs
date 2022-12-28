using BookManagement.Core.Domain.Queries.PublisherQueries.Inputs;
using BookManagement.Core.Domain.Queries.PublisherQueries.QueryResults;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.Services.PublisherServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Publishers;

public class IndexModel : PaginationPageModel
{
    private readonly IPublisherPageService _publisherPageService;
    public IPaginateQueryResult<PublisherQueryResult> Publishers { get; set; } = default!;

    public IndexModel(IPublisherPageService publisherPageService)
    {
        _publisherPageService = publisherPageService;
    }

    [BindProperty(SupportsGet = true)]
    public string Name { get; set; }

    public async Task OnGetAsync()
    {
        var filter = new FilterPublisherQueries
        {
            CurrentPage = CurrentPage,
            Name = Name,
            OrderBy = Sort,
            PageSize = 5
        };
        Publishers = await _publisherPageService.GetPaginatedAsync(filter);
    }

    public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
        AddPaginationData("name", Name);
        AddPaginationData("page", Publishers?.CurrentPage.ToString());
        base.OnPageHandlerExecuted(context);
    }
}