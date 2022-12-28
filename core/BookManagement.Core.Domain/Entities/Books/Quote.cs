using BookManagement.Core.Domain.Entities.Books.Validations;
using BookManagement.Core.Shared.Entities;

namespace BookManagement.Core.Domain.Entities.Books;

public class Quote : BaseEntity<QuoteValidation>
{
    public Quote(string description)
    {
        Description = description;
    }

    public string Description { get; private set; }
    public virtual Book Book { get; private set; }

    public void Edit(string description)
    {
        Description = description;
        SetUpdatedDate(DateTime.Now);
    }

    public void SetBook(Book book)
    {
        Book = book;
    }
}