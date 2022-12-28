using Bogus;
using BookManagement.Core.Domain.Entities.Publishers;
using static BookManagement.Core.Domain.Entities.Publishers.Specifications.PublisherSpecification;

namespace BookManagement.Core.Domain.UnitTest.Entities.Publishers;

[CollectionDefinition(nameof(PublisherTestsCollection))]
public class PublisherTestsCollection : ICollectionFixture<PublisherTestsFixture>
{
}

public class PublisherTestsFixture
{
    public Publisher GetValidPublisher()
    {
        return new Faker<Publisher>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);

                return new Publisher(name, description);
            });
    }

    public Publisher GetInvalidPublisher()
    {
        return new Faker<Publisher>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize + 1, NameColumnSize * 2);
                var description = x.Random.String(DescriptionColumnSize + 1, DescriptionColumnSize * 2);

                return new Publisher(name, description);
            });
    }
}