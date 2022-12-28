using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Core.Shared.Extensions.StringExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BookManagement.Modules.Web.Models.BookModels;

public class BookModel
{
    [Key] public Guid? Id { get; set; }

    [Required]
    [DisplayName("Title")]
    [MaxLength(BookSpecification.TitleColumnSize)]
    public string Title { get; set; }

    [DisplayName("Description")]
    [MaxLength(BookSpecification.DescriptionColumnSize)]
    public string Description { get; set; }

    [Required] public Format Format { get; set; }

    [DisplayName("Publication Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
    public DateTime? PublicationDate { get; set; }

    [DisplayName("ISBN-10")]
    [Range(BookSpecification.Isbn10Length, BookSpecification.Isbn10Length)]
    public string Isbn10 { get; set; }

    [DisplayName("ISBN-13")]
    [Range(BookSpecification.Isbn13Length, BookSpecification.Isbn13Length)]
    public string Isbn13 { get; set; }

    [Required] [DisplayName("Publisher")] public Guid PublisherId { get; set; }

    [Required] [DisplayName("Category")] public Guid CategoryId { get; set; }

    public IEnumerable<BookKeywordModel> Keywords { get; set; }
    public IEnumerable<BookQuoteModel> Quotes { get; set; }
    public IEnumerable<BookReviewModel> Reviews { get; set; }

    public static BookModel FromQueryResult(BookQueryResult bookQueryResult)
    {
        return new BookModel
        {
            Id = bookQueryResult.Id,
            Title = bookQueryResult.Title,
            Description = bookQueryResult.Description,
            CategoryId = bookQueryResult.CategoryId,
            PublisherId = bookQueryResult.PublisherId,
            Format = bookQueryResult.Format,
            Isbn10 = bookQueryResult.Isbn10,
            Isbn13 = bookQueryResult.Isbn13,
            PublicationDate = bookQueryResult.PublicationDate.ToDateTime(),
            Keywords = bookQueryResult.Keywords.Select(x => new BookKeywordModel(x.Id, x.Description)),
            Quotes = bookQueryResult.Quotes.Select(x => new BookQuoteModel(x.Id, x.Description)),
            Reviews = bookQueryResult.Reviews.Select(x =>
                new BookReviewModel(x.Id, x.Description, x.ReviewType, x.AuthorName, x.AuthorNameInfo))
        };
    }
}