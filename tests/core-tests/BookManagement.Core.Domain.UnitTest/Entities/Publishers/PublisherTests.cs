namespace BookManagement.Core.Domain.UnitTest.Entities.Publishers;

[Collection(nameof(PublisherTestsCollection))]
public class PublisherTests
{
    private readonly PublisherTestsFixture _fixture;

    public PublisherTests(PublisherTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Valid publisher")]
    [Trait("Category", "Publisher Entity")]
    public void PublisherIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        //Arrange
        var publisher = _fixture.GetValidPublisher();

        //Act
        var resultValidation = publisher.Validate();

        //Assert
        Assert.True(resultValidation.IsValid);
        Assert.Empty(resultValidation.Errors);
    }

    [Fact(DisplayName = "Invalid publisher")]
    [Trait("Category", "Publisher Entity")]
    public void PublisherIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        //Arrange
        var publisher = _fixture.GetInvalidPublisher();

        //Act
        var resultValidation = publisher.Validate();

        //Assert
        Assert.False(resultValidation.IsValid);
        Assert.Equal(2, resultValidation.Errors.Count);
    }
}