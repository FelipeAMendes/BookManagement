using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Modules.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Modules.Web.ViewComponents;

public class PaginationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(IPaginateQueryResult<object> paginatedValues)
    {
        return View("Default", new PaginationModel
        {
            List = paginatedValues,
            RouteData = null
        });
    }
}