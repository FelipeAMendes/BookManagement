using BookManagement.Core.Domain.Queries.BookQueries.Inputs;
using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.Services.BookServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Books;

public class IndexModel : PaginationPageModel
{
    private readonly IBookPageService _bookPageService;
    public IPaginateQueryResult<BookQueryResult> Books { get; set; } = default!;

    public IndexModel(IBookPageService bookPageService)
    {
        _bookPageService = bookPageService;
    }

    [BindProperty(SupportsGet = true)]
    public string Title { get; set; }

    public async Task OnGetAsync()
    {
        var filter = new FilterBookQueries
        {
            CurrentPage = CurrentPage,
            Title = Title,
            OrderBy = Sort,
            PageSize = 5
        };

        Books = await _bookPageService.GetPaginatedAsync(filter);
    }

    public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
        AddPaginationData("title", Title);
        AddPaginationData("page", Books?.CurrentPage.ToString());
        base.OnPageHandlerExecuted(context);
    }
}