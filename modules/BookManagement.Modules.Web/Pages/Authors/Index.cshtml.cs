using BookManagement.Core.Domain.Queries.AuthorQueries.Inputs;
using BookManagement.Core.Domain.Queries.AuthorQueries.QueryResults;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.Services.AuthorServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Authors;

public class IndexModel : PaginationPageModel
{
    private readonly IAuthorPageService _authorPageService;
    public IPaginateQueryResult<AuthorQueryResult> Authors { get; set; } = default!;

    public IndexModel(IAuthorPageService authorPageService)
    {
        _authorPageService = authorPageService;
    }

    [BindProperty(SupportsGet = true)]
    public string Name { get; set; }

    public async Task OnGetAsync()
    {
        var filter = new FilterAuthorQueries
        {
            CurrentPage = CurrentPage,
            Name = Name,
            OrderBy = Sort,
            PageSize = 5
        };

        Authors = await _authorPageService.GetPaginatedAsync(filter);
    }

    public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
        AddPaginationData("name", Name);
        AddPaginationData("page", Authors?.CurrentPage.ToString());
        base.OnPageHandlerExecuted(context);
    }
}
