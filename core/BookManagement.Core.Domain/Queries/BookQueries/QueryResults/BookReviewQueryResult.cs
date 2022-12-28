using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Shared.Extensions.EnumExtensions;

namespace BookManagement.Core.Domain.Queries.BookQueries.QueryResults;

public class BookReviewQueryResult
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public string AuthorNameInfo { get; set; }
    public ReviewType ReviewType { get; set; }

    public string ReviewTypeDescription => ReviewType.GetDescription();
}