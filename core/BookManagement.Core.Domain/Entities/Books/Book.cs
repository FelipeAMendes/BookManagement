using BookManagement.Core.Domain.Entities.Authors;
using BookManagement.Core.Domain.Entities.Books.Validations;
using BookManagement.Core.Domain.Entities.Categories;
using BookManagement.Core.Domain.Entities.Publishers;
using BookManagement.Core.Shared.Entities;

namespace BookManagement.Core.Domain.Entities.Books;

public class Book : BaseEntity<BookValidation>
{
    private List<Keyword> _keywords;
    private List<Quote> _quotes;
    private List<Review> _reviews;

    public Book(string title, string description, DateTime? publicationDate, Format format, Isbn10? isbn10, Isbn13? isbn13)
    {
        Title = title;
        Description = description;
        PublicationDate = publicationDate;
        Format = format;
        Isbn10 = isbn10;
        Isbn13 = isbn13;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public Format Format { get; private set; }
    public DateTime? PublicationDate { get; private set; }
    public Isbn10? Isbn10 { get; private set; }
    public Isbn13? Isbn13 { get; private set; }

    public virtual Author Author { get; private set; }
    public virtual Publisher Publisher { get; private set; }
    public virtual Category Category { get; private set; }
    public virtual IReadOnlyCollection<Keyword> Keywords => _keywords?.AsReadOnly();
    public virtual IReadOnlyCollection<Quote> Quotes => _quotes?.AsReadOnly();
    public virtual IReadOnlyCollection<Review> Reviews => _reviews?.AsReadOnly();

    public void AddKeyword(Keyword keyword)
    {
        _keywords ??= new List<Keyword>();

        var keywordExists = _keywords.Any(x => x.Id == keyword.Id);

        if (!keywordExists)
            _keywords.Add(keyword);
    }

    public void AddQuote(Quote quote)
    {
        _quotes ??= new List<Quote>();

        var quoteExists = _quotes.Any(x => x.Id == quote.Id);

        if (!quoteExists)
            _quotes.Add(quote);
    }

    public void AddReview(Review review)
    {
        _reviews ??= new List<Review>();

        var reviewExists = _reviews.Any(x => x.Id == review.Id);

        if (!reviewExists)
            _reviews.Add(review);
    }

    public void SetAuthor(Author author)
    {
        Author = author;
    }

    public void SetCategory(Category category)
    {
        Category = category;
    }

    public void SetPublisher(Publisher publisher)
    {
        Publisher = publisher;
    }

    public void Edit(string title, string description, DateTime? publicationDate, Format format, Isbn10? isbn10,
        Isbn13? isbn13)
    {
        Title = title;
        Description = description;
        PublicationDate = publicationDate;
        Format = format;
        Isbn10 = isbn10;
        Isbn13 = isbn13;
        SetUpdatedDate(DateTime.Now);
    }
}