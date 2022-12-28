using BookManagement.Core.Domain.Queries.BookQueries.Inputs;
using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Modules.Web.Services.BookServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages;

public class IndexModel : PageModel
{
    private const int BookIndexPageSize = 10;
    private readonly IBookPageService _bookPageService;

    public IndexModel(IBookPageService bookPageService)
    {
        _bookPageService = bookPageService;
    }

    public IEnumerable<BookQueryResult> BookQueryResult { get; set; } = new List<BookQueryResult>();

    public async Task<IActionResult> OnGetAsync()
    {
        var paginatedResult = await _bookPageService.GetPaginatedAsync(new FilterBookQueries { PageSize = BookIndexPageSize, CurrentPage = 1 });
        BookQueryResult = paginatedResult.Items;
        return Page();
    }
}