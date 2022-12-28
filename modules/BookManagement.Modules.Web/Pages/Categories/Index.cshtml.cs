using BookManagement.Core.Domain.Queries.CategoryQueries.Inputs;
using BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Modules.Web.Models;
using BookManagement.Modules.Web.Services.CategoryServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Pages.Categories;

public class IndexModel : PaginationPageModel
{
    private readonly ICategoryPageService _categoryPageService;
    public IPaginateQueryResult<CategoryQueryResult> Categories { get; set; } = default!;

    public IndexModel(ICategoryPageService categoryPageService)
    {
        _categoryPageService = categoryPageService;
    }

    [BindProperty(SupportsGet = true)]
    public string Name { get; set; }

    public async Task OnGetAsync()
    {
        var filter = new FilterCategoryQueries
        {
            CurrentPage = CurrentPage,
            Name = Name,
            OrderBy = Sort,
            PageSize = 5
        };

        Categories = await _categoryPageService.GetPaginatedAsync(filter);
    }

    public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
        AddPaginationData("name", Name);
        AddPaginationData("page", Categories?.CurrentPage.ToString());
        base.OnPageHandlerExecuted(context);
    }
}