using Bogus;
using BookManagement.Core.Domain.Commands.PublisherCommands;
using static BookManagement.Core.Domain.Entities.Publishers.Specifications.PublisherSpecification;

namespace BookManagement.Core.Domain.UnitTest.Commands.PublisherCommands;

[CollectionDefinition(nameof(PublisherCommandTestsCollection))]
public class PublisherCommandTestsCollection : ICollectionFixture<PublisherCommandTestsFixture>
{
}

public class PublisherCommandTestsFixture
{
    public CreatePublisherCommand GetValidCreatePublisherCommand()
    {
        return new Faker<CreatePublisherCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);

                return new CreatePublisherCommand
                {
                    Name = name,
                    Description = description
                };
            });
    }

    public CreatePublisherCommand GetInvalidCreatePublisherCommand()
    {
        return new Faker<CreatePublisherCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize + 1, NameColumnSize * 2);
                var description = x.Random.String2(DescriptionColumnSize + 1, DescriptionColumnSize * 2);

                return new CreatePublisherCommand
                {
                    Name = name,
                    Description = description
                };
            });
    }

    public UpdatePublisherCommand GetValidUpdatePublisherCommand()
    {
        return new Faker<UpdatePublisherCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);

                return new UpdatePublisherCommand
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description
                };
            });
    }

    public UpdatePublisherCommand GetInvalidUpdatePublisherCommand()
    {
        return new Faker<UpdatePublisherCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize + 1, NameColumnSize * 2);
                var description = x.Random.String2(DescriptionColumnSize + 1, DescriptionColumnSize * 2);

                return new UpdatePublisherCommand
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description
                };
            });
    }

    public DeletePublisherCommand GetValidDeletePublisherCommand()
    {
        return new Faker<DeletePublisherCommand>()
            .CustomInstantiator(_ => new DeletePublisherCommand(Guid.NewGuid()));
    }

    public DeletePublisherCommand GetInvalidDeletePublisherCommand()
    {
        return new Faker<DeletePublisherCommand>()
            .CustomInstantiator(_ => new DeletePublisherCommand(Guid.Empty));
    }
}