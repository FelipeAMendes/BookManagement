using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using System.Collections.Generic;

namespace BookManagement.Modules.Web.Models;

public class PaginationModel
{
    public int LimitPaginationLinks { get; set; } = 10;
    public IPaginateQueryResult<object> List { get; set; }
    public Dictionary<string, string> RouteData { get; set; }
}