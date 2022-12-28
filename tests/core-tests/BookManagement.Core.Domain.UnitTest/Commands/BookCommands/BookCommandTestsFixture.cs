using Bogus;
using BookManagement.Core.Domain.Commands.BookCommands;
using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using static BookManagement.Core.Domain.Entities.Books.Specifications.BookSpecification;

namespace BookManagement.Core.Domain.UnitTest.Commands.BookCommands;

[CollectionDefinition(nameof(BookCommandTestsCollection))]
public class BookCommandTestsCollection : ICollectionFixture<BookCommandTestsFixture>
{
}

public class BookCommandTestsFixture
{
    public CreateBookCommand GetValidCreateBookCommand()
    {
        return new Faker<CreateBookCommand>()
            .CustomInstantiator(x =>
            {
                var title = x.Random.String2(TitleColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);
                var format = x.Random.Enum<Format>();
                var publicationDate = x.Date.Between(DateTime.Today.AddYears(-100), DateTime.Today);
                var quotes = new List<CreateBookQuoteCommand>
                {
                    new() {Description = x.Random.String2(QuoteSpecification.DescriptionColumnSize)}
                };
                var keywords = new List<CreateBookKeywordCommand>
                {
                    new() {Description = x.Random.String2(KeywordSpecification.DescriptionColumnSize)}
                };
                var reviews = new List<CreateBookReviewCommand>
                {
                    new()
                    {
                        AuthorName = x.Person.FullName,
                        AuthorNameInfo = x.Lorem.Paragraph(),
                        ReviewType = x.Random.Enum<ReviewType>(),
                        Description = x.Random.String2(ReviewSpecification.DescriptionColumnSize)
                    }
                };

                return new CreateBookCommand
                {
                    Title = title,
                    Description = description,
                    Format = format,
                    PublicationDate = publicationDate,
                    CategoryId = Guid.NewGuid(),
                    PublisherId = Guid.NewGuid(),
                    Quotes = quotes,
                    Keywords = keywords,
                    Reviews = reviews
                };
            });
    }

    public CreateBookCommand GetInvalidCreateBookCommand()
    {
        return new Faker<CreateBookCommand>()
            .CustomInstantiator(x =>
            {
                var title = x.Random.String2(TitleColumnSize + 1, TitleColumnSize * 2);
                var description = x.Random.String2(DescriptionColumnSize + 1, DescriptionColumnSize * 2);

                return new CreateBookCommand
                {
                    Title = title,
                    Description = description
                };
            });
    }

    public Book GetBookEntity()
    {
        return new Faker<Book>()
            .CustomInstantiator(x =>
            {
                var title = x.Random.String2(TitleColumnSize, TitleColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);
                var format = x.Random.Enum<Format>();

                return new Book(title, description, DateTime.Today, format, null, null);
            });
    }

    public UpdateBookCommand GetValidUpdateBookCommand()
    {
        return new Faker<UpdateBookCommand>()
            .CustomInstantiator(x =>
            {
                var title = x.Random.String2(TitleColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);
                var format = x.Random.Enum<Format>();
                var publicationDate = x.Date.Between(DateTime.Today.AddYears(-100), DateTime.Today);
                var quotes = new List<UpdateBookQuoteCommand>
                {
                    new()
                    {
                        Id = Guid.NewGuid(), Description = x.Random.String2(QuoteSpecification.DescriptionColumnSize)
                    }
                };
                var keywords = new List<UpdateBookKeywordCommand>
                {
                    new()
                    {
                        Id = Guid.NewGuid(), Description = x.Random.String2(KeywordSpecification.DescriptionColumnSize)
                    }
                };
                var reviews = new List<UpdateBookReviewCommand>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        AuthorName = x.Person.FullName,
                        AuthorNameInfo = x.Lorem.Paragraph(),
                        ReviewType = x.Random.Enum<ReviewType>(),
                        Description = x.Random.String2(ReviewSpecification.DescriptionColumnSize)
                    }
                };

                return new UpdateBookCommand
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    Description = description,
                    Format = format,
                    PublicationDate = publicationDate,
                    CategoryId = Guid.NewGuid(),
                    PublisherId = Guid.NewGuid(),
                    Quotes = quotes,
                    Keywords = keywords,
                    Reviews = reviews
                };
            });
    }

    public UpdateBookCommand GetInvalidUpdateBookCommand()
    {
        return new Faker<UpdateBookCommand>()
            .CustomInstantiator(x =>
            {
                var title = x.Random.String2(TitleColumnSize + 1, TitleColumnSize * 2);
                var description = x.Random.String2(DescriptionColumnSize + 1, DescriptionColumnSize * 2);

                return new UpdateBookCommand
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    Description = description
                };
            });
    }

    public DeleteBookCommand GetValidDeleteBookCommand()
    {
        return new Faker<DeleteBookCommand>()
            .CustomInstantiator(_ => new DeleteBookCommand(Guid.NewGuid()));
    }

    public DeleteBookCommand GetInvalidDeleteBookCommand()
    {
        return new Faker<DeleteBookCommand>()
            .CustomInstantiator(_ => new DeleteBookCommand(Guid.Empty));
    }
}