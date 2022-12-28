using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Queries.PublisherQueries.QueryResults;

public class PublisherQueryResult : IQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}