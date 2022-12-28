using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Queries.AuthorQueries.QueryResults;

public class AuthorQueryResult : IQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}