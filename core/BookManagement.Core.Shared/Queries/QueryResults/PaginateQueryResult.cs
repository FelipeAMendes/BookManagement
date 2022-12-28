using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Shared.Queries.QueryResults;

public class PaginateQueryResult<TQueryResult> : IPaginateQueryResult<TQueryResult>
{
    public PaginateQueryResult(int currentPage, int pages, int pageSize, long total, string orderBy, IEnumerable<TQueryResult> items)
    {
        if (pages == 0)
            pages = 1;

        CurrentPage = currentPage;
        Pages = pages;
        PageSize = pageSize;
        Total = total;
        OrderBy = orderBy;
        Items = items ?? Array.Empty<TQueryResult>();
    }

    public int CurrentPage { get; set; }
    public int Pages { get; set; }
    public int PageSize { get; set; }
    public long Total { get; set; }
    public string OrderBy { get; set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < Pages;
    public IEnumerable<TQueryResult> Items { get; set; }

    // Check a better way
    public PaginateQueryResult<object> ToObjectQueryResult()
    {
        return new PaginateQueryResult<object>(CurrentPage, Pages, PageSize, Total, OrderBy, Items.Cast<object>());
    }
}