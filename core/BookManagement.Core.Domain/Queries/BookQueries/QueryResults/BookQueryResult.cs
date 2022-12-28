using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Shared.Extensions.EnumExtensions;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Queries.BookQueries.QueryResults;

public class BookQueryResult : IQueryResult
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Format Format { get; set; }
    public string PublicationDate { get; set; }
    public string Isbn10 { get; set; }
    public string Isbn13 { get; set; }
    public Guid PublisherId { get; set; }
    public Guid CategoryId { get; set; }

    public string PublisherName { get; set; }
    public string CategoryName { get; set; }
    public string FormatDescription => Format.GetDescription();

    public IEnumerable<BookKeywordQueryResult> Keywords { get; set; }
    public IEnumerable<BookQuoteQueryResult> Quotes { get; set; }
    public IEnumerable<BookReviewQueryResult> Reviews { get; set; }
}