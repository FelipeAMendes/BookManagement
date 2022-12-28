using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;

public class CategoryDetailsQueryResult : IQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryQueryResult ParentCategory { get; set; }
    public IReadOnlyCollection<CategoryQueryResult> ChildCategories { get; set; }
}