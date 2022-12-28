using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;

public class CategoryQueryResult : IQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public IEnumerable<string> ParentCategoryNames { get; set; }
}