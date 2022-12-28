namespace BookManagement.Core.Domain.UnitTest.Entities.Authors;

[Collection(nameof(AuthorTestsCollection))]
public class AuthorTests
{
    private readonly AuthorTestsFixture _fixture;

    public AuthorTests(AuthorTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Valid author")]
    [Trait("Category", "Author Entity")]
    public void AuthorIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        //Arrange
        var author = _fixture.GetValidAuthor();

        //Act
        var resultValidation = author.Validate();

        //Assert
        Assert.True(resultValidation.IsValid);
        Assert.Empty(resultValidation.Errors);
    }

    [Fact(DisplayName = "Invalid author")]
    [Trait("Category", "Author Entity")]
    public void AuthorIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        //Arrange
        var author = _fixture.GetInvalidAuthor();

        //Act
        var resultValidation = author.Validate();

        //Assert
        Assert.False(resultValidation.IsValid);
        Assert.Equal(2, resultValidation.Errors.Count);
    }
}