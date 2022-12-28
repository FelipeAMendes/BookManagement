using BookManagement.Core.Domain.Entities.Books.Validations;
using BookManagement.Core.Shared.Entities;

namespace BookManagement.Core.Domain.Entities.Books;

public class Review : BaseEntity<ReviewValidation>
{
    public Review(string description, ReviewType reviewType, string authorName, string authorNameInfo)
    {
        Description = description;
        ReviewType = reviewType;
        AuthorName = authorName;
        AuthorNameInfo = authorNameInfo;
    }

    public string Description { get; private set; }
    public string AuthorName { get; private set; }
    public string AuthorNameInfo { get; private set; }
    public ReviewType ReviewType { get; private set; }
    public virtual Book Book { get; private set; }

    public void Edit(string description, ReviewType reviewType, string authorName, string authorNameInfo)
    {
        Description = description;
        ReviewType = reviewType;
        AuthorName = authorName;
        AuthorNameInfo = authorNameInfo;
        SetUpdatedDate(DateTime.Now);
    }

    public void SetBook(Book book)
    {
        Book = book;
    }
}