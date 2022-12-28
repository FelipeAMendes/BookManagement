using BookManagement.Core.Domain.Commands.BookCommands;
using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Modules.Web.Models.BookModels;

public class BookReviewModel
{
    public BookReviewModel(Guid id, string description, ReviewType reviewType, string authorName, string authorNameInfo)
    {
        Id = id;
        Description = description;
        ReviewType = reviewType;
        AuthorName = authorName;
        AuthorNameInfo = authorNameInfo;
    }

    [Key] public Guid? Id { get; set; }

    [Required]
    [DisplayName("Description")]
    [MaxLength(ReviewSpecification.DescriptionColumnSize)]
    public string Description { get; set; }

    [Required]
    [DisplayName("Author Name")]
    [MaxLength(ReviewSpecification.AuthorNameColumnSize)]
    public string AuthorName { get; set; }

    [DisplayName("Author Name Info")]
    [MaxLength(ReviewSpecification.AuthorNameInfoColumnSize)]
    public string AuthorNameInfo { get; set; }

    [Required] public ReviewType ReviewType { get; set; }

    public CreateBookReviewCommand ToCreateCommand()
    {
        return new CreateBookReviewCommand
        {
            Description = Description,
            ReviewType = ReviewType,
            AuthorName = AuthorName,
            AuthorNameInfo = AuthorNameInfo
        };
    }

    public UpdateBookReviewCommand ToUpdateCommand()
    {
        return new UpdateBookReviewCommand
        {
            Id = Id ?? Guid.Empty,
            Description = Description,
            ReviewType = ReviewType,
            AuthorName = AuthorName,
            AuthorNameInfo = AuthorNameInfo
        };
    }
}