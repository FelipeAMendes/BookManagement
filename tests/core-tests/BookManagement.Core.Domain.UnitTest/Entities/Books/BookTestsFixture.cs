using Bogus;
using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using static BookManagement.Core.Domain.Entities.Books.Specifications.BookSpecification;

namespace BookManagement.Core.Domain.UnitTest.Entities.Books;

[CollectionDefinition(nameof(BookTestsCollection))]
public class BookTestsCollection : ICollectionFixture<BookTestsFixture> { }

public class BookTestsFixture
{
    public Book GetValidBook()
    {
        return new Faker<Book>()
            .CustomInstantiator(x =>
            {
                var title = x.Random.String2(TitleColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);
                var publicationDate = DateTime.Now;
                var format = x.Random.Enum<Format>();
                var quote = new Quote(x.Random.String2(QuoteSpecification.DescriptionColumnSize));
                var keyword = new Keyword(x.Random.String2(KeywordSpecification.DescriptionColumnSize));
                var review = new Review(
                    authorName: x.Person.FullName,
                    authorNameInfo: x.Lorem.Paragraph(),
                    reviewType: x.Random.Enum<ReviewType>(),
                    description: x.Random.String2(ReviewSpecification.DescriptionColumnSize));

                var book = new Book(title, description, publicationDate, format, ValidIsbn10, ValidIsbn13);
                book.AddQuote(quote);
                book.AddKeyword(keyword);
                book.AddReview(review);
                return book;
            });
    }

    public Book GetInvalidBook()
    {
        return new Faker<Book>()
            .CustomInstantiator(x =>
            {
                var title = x.Random.String2(TitleColumnSize, TitleColumnSize * 2);
                var description = x.Random.String(DescriptionColumnSize + 1, DescriptionColumnSize * 2);
                var publicationDate = DateTime.Now;
                var format = x.Random.Enum<Format>();
                var quote = new Quote(null);
                var keyword = new Keyword(null);
                var review = new Review(
                    authorName: null,
                    authorNameInfo: null,
                    reviewType: x.Random.Enum<ReviewType>(),
                    description: null);

                var book = new Book(title, description, publicationDate, format, ValidIsbn10, ValidIsbn13);
                book.AddQuote(quote);
                book.AddKeyword(keyword);
                book.AddReview(review);
                return book;
            });
    }

    public Book GetBookWithInvalidIsbn(string isbn10, string isbn13)
    {
        return new Faker<Book>()
            .CustomInstantiator(x =>
            {
                var title = x.Random.String2(TitleColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);
                var publicationDate = DateTime.Now;
                var format = x.Random.Enum<Format>();

                return new Book(title, description, publicationDate, format, isbn10, isbn13);
            });
    }

    public Review GetValidReview()
    {
        return new Faker<Review>()
            .CustomInstantiator(x =>
            {
                var description = x.Random.String2(DescriptionColumnSize);
                var reviewType = x.Random.Enum<ReviewType>();
                var authorName = x.Name.FullName();
                var authorNameInfo = x.Name.FullName();

                return new Review(description, reviewType, authorName, authorNameInfo);
            });
    }
}