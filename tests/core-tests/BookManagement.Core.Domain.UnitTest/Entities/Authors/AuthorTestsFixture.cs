using Bogus;
using BookManagement.Core.Domain.Entities.Authors;
using static BookManagement.Core.Domain.Entities.Authors.Specifications.AuthorSpecification;

namespace BookManagement.Core.Domain.UnitTest.Entities.Authors;

[CollectionDefinition(nameof(AuthorTestsCollection))]
public class AuthorTestsCollection : ICollectionFixture<AuthorTestsFixture>
{
}

public class AuthorTestsFixture
{
    public Author GetValidAuthor()
    {
        return new Faker<Author>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);

                return new Author(name, description);
            });
    }

    public Author GetInvalidAuthor()
    {
        return new Faker<Author>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize, NameColumnSize * 2);
                var description = x.Random.String2(DescriptionColumnSize + 1, DescriptionColumnSize * 2);

                return new Author(name, description);
            });
    }
}