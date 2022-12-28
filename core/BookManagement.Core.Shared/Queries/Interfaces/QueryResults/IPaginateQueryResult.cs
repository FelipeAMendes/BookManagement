using BookManagement.Core.Shared.Queries.QueryResults;

namespace BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

public interface IPaginateQueryResult<TQueryResult> : IQueryResult
{
    int CurrentPage { get; set; }
    int Pages { get; set; }
    int PageSize { get; set; }
    long Total { get; set; }
    string OrderBy { get; set; }
    bool HasPrevious { get; }
    bool HasNext { get; }
    IEnumerable<TQueryResult> Items { get; set; }

    PaginateQueryResult<object> ToObjectQueryResult();
}