namespace BookManagement.Core.Domain.Entities.Books.Specifications;

public static class BookSpecification
{
    public const int TitleColumnSize = 300;
    public const int DescriptionColumnSize = 5000;
    public const int Isbn10Length = 10;
    public const int Isbn13Length = 13;

    public const string ValidIsbn10 = "5734395951";
    public const string ValidIsbn13 = "9781234567897";
}