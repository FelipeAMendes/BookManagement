using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using FluentAssertions;

namespace BookManagement.Core.Domain.UnitTest.Entities.Books;

[Collection(nameof(BookTestsCollection))]
public class BookTests
{
    private readonly BookTestsFixture _fixture;

    public BookTests(BookTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Valid book")]
    [Trait("Category", "Book Entity")]
    public void BookIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        //Arrange
        var book = _fixture.GetValidBook();

        //Act
        var resultValidation = book.Validate();

        //Assert
        book.Quotes.Select(x => x.Validate().IsValid).Should().OnlyContain(x => true);
        book.Reviews.Select(x => x.Validate().IsValid).Should().OnlyContain(x => true);
        book.Keywords.Select(x => x.Validate().IsValid).Should().OnlyContain(x => true);
        Assert.True(resultValidation.IsValid);
        Assert.Empty(resultValidation.Errors);
    }

    [Fact(DisplayName = "Invalid book")]
    [Trait("Category", "Book Entity")]
    public void BookIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        //Arrange
        var book = _fixture.GetInvalidBook();

        //Act
        var resultValidation = book.Validate();

        //Assert
        book.Quotes.Select(x => x.Validate().Errors).Should().HaveCountGreaterThan(0);
        book.Reviews.Select(x => x.Validate().Errors).Should().HaveCountGreaterThan(0);
        book.Keywords.Select(x => x.Validate().Errors).Should().HaveCountGreaterThan(0);
        Assert.False(resultValidation.IsValid);
        Assert.Equal(2, resultValidation.Errors.Count);
    }

    [Fact(DisplayName = "Invalid ISBN10")]
    [Trait("Category", "Book Entity")]
    public void BookIsNotValid_NewBookInstanceCreated_ThrowsIsbn10ArgumentException()
    {
        //Arrange
        const string invalidIsbn10 = "1234567891";

        //Act
        void Action()
        {
            _fixture.GetBookWithInvalidIsbn(invalidIsbn10, BookSpecification.ValidIsbn13);
        }

        //Assert
        Assert.Throws<ArgumentException>(Action);
    }

    [Fact(DisplayName = "Invalid ISBN13")]
    [Trait("Category", "Book Entity")]
    public void BookIsNotValid_NewBookInstanceCreated_ThrowsIsbn13ArgumentException()
    {
        //Arrange
        const string invalidIsbn13 = "1231231231231";

        //Act
        void Action()
        {
            _fixture.GetBookWithInvalidIsbn(BookSpecification.ValidIsbn10, invalidIsbn13);
        }

        //Assert
        Assert.Throws<ArgumentException>(Action);
    }

    [Fact(DisplayName = "Don't add repeated keyword")]
    [Trait("Category", "Book Entity")]
    public void BookIsValid_AddKeywordMethodIsCalled_DontAddRepeated()
    {
        //Arrange
        var book = _fixture.GetValidBook();
        var keyword = new Keyword("Keyword 01");

        //Act
        book.AddKeyword(keyword);
        book.AddKeyword(keyword);

        //Assert
        Assert.Equal(2, book.Keywords.Count);
    }

    [Fact(DisplayName = "Don't add repeated quote")]
    [Trait("Category", "Book Entity")]
    public void BookIsValid_AddQuoteMethodIsCalled_DontAddRepeated()
    {
        //Arrange
        var book = _fixture.GetValidBook();
        var quote = new Quote("Quote 01");

        //Act
        book.AddQuote(quote);
        book.AddQuote(quote);

        //Assert
        Assert.Equal(2, book.Quotes.Count);
    }

    [Fact(DisplayName = "Don't add repeated review")]
    [Trait("Category", "Book Entity")]
    public void BookIsValid_AddReviewMethodIsCalled_DontAddRepeated()
    {
        //Arrange
        var book = _fixture.GetValidBook();
        var review = _fixture.GetValidReview();

        //Act
        book.AddReview(review);
        book.AddReview(review);

        //Assert
        Assert.Equal(2, book.Reviews.Count);
    }

    [Fact(DisplayName = "Change properties keyword")]
    [Trait("Category", "Book Entity - Keyword")]
    public void KeywordIsValid_EditMethodIsCalled_ChangeValues()
    {
        //Arrange
        var keyword = new Keyword("Keyword 01");

        //Act
        const string description = "Keyword 02";
        keyword.Edit(description);

        //Assert
        Assert.Equal(description, keyword.Description);
        Assert.NotEqual(DateTime.MinValue, keyword.UpdatedDate);
    }

    [Fact(DisplayName = "Change properties quote")]
    [Trait("Category", "Book Entity - Quote")]
    public void QuoteIsValid_EditMethodIsCalled_ChangeValues()
    {
        //Arrange
        var quote = new Quote("Quote 01");

        //Act
        const string description = "Quote 02";
        quote.Edit(description);

        //Assert
        Assert.Equal(description, quote.Description);
        Assert.NotEqual(DateTime.MinValue, quote.UpdatedDate);
    }

    [Fact(DisplayName = "Change properties review")]
    [Trait("Category", "Book Entity - Review")]
    public void ReviewIsValid_EditMethodIsCalled_ChangeValues()
    {
        //Arrange
        var review = _fixture.GetValidReview();

        //Act
        const string description = "Review 02";
        const ReviewType reviewType = ReviewType.InsideFlat;
        const string authorName = "Author 02";
        const string authorNameInfo = "AuthorInfo 02";
        review.Edit(description, reviewType, authorName, authorNameInfo);

        //Assert
        Assert.Equal(description, review.Description);
        Assert.Equal(reviewType, review.ReviewType);
        Assert.Equal(authorName, review.AuthorName);
        Assert.Equal(authorNameInfo, review.AuthorNameInfo);
        Assert.NotEqual(DateTime.MinValue, review.UpdatedDate);
    }

    [Fact(DisplayName = "Change book Keyword")]
    [Trait("Category", "Book Entity - Keyword")]
    public void KeywordIsChanged_EditMethodIsCalled_ChangeObject()
    {
        //Arrange
        var keyword = new Keyword("Keyword 01");

        //Act
        Assert.Null(keyword.Book);
        keyword.SetBook(_fixture.GetValidBook());

        //Assert
        Assert.NotNull(keyword.Book);
    }

    [Fact(DisplayName = "Change book Quote")]
    [Trait("Category", "Book Entity - Quote")]
    public void QuoteIsChanged_EditMethodIsCalled_ChangeObject()
    {
        //Arrange
        var quote = new Quote("Quote 01");

        //Act
        Assert.Null(quote.Book);
        quote.SetBook(_fixture.GetValidBook());

        //Assert
        Assert.NotNull(quote.Book);
    }

    [Fact(DisplayName = "Change book Review")]
    [Trait("Category", "Book Entity - Review")]
    public void ReviewIsChanged_EditMethodIsCalled_ChangeObject()
    {
        //Arrange
        var review = _fixture.GetValidReview();

        //Act
        Assert.Null(review.Book);
        review.SetBook(_fixture.GetValidBook());

        //Assert
        Assert.NotNull(review.Book);
    }
}