using Bogus;
using BookManagement.Core.Domain.Commands.AuthorCommands;
using static BookManagement.Core.Domain.Entities.Authors.Specifications.AuthorSpecification;

namespace BookManagement.Core.Domain.UnitTest.Commands.AuthorCommands;

[CollectionDefinition(nameof(AuthorCommandTestsCollection))]
public class AuthorCommandTestsCollection : ICollectionFixture<AuthorCommandTestsFixture> { }

public class AuthorCommandTestsFixture
{
    public CreateAuthorCommand GetValidCreateAuthorCommand()
    {
        return new Faker<CreateAuthorCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);

                return new CreateAuthorCommand
                {
                    Name = name,
                    Description = description
                };
            });
    }

    public CreateAuthorCommand GetInvalidCreateAuthorCommand()
    {
        return new Faker<CreateAuthorCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize + 1, NameColumnSize * 2);
                var description = x.Random.String2(DescriptionColumnSize + 1, DescriptionColumnSize * 2);

                return new CreateAuthorCommand
                {
                    Name = name,
                    Description = description
                };
            });
    }

    public UpdateAuthorCommand GetValidUpdateAuthorCommand()
    {
        return new Faker<UpdateAuthorCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);

                return new UpdateAuthorCommand
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description
                };
            });
    }

    public UpdateAuthorCommand GetInvalidUpdateAuthorCommand(int nameColumnSize)
    {
        return new Faker<UpdateAuthorCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(nameColumnSize + 1, NameColumnSize * 2);
                var description = x.Random.String2(DescriptionColumnSize + 1, DescriptionColumnSize * 2);

                return new UpdateAuthorCommand
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description
                };
            });
    }

    public DeleteAuthorCommand GetValidDeleteAuthorCommand()
    {
        return new Faker<DeleteAuthorCommand>()
            .CustomInstantiator(_ => new DeleteAuthorCommand(Guid.NewGuid()));
    }

    public DeleteAuthorCommand GetInvalidDeleteAuthorCommand()
    {
        return new Faker<DeleteAuthorCommand>()
            .CustomInstantiator(_ => new DeleteAuthorCommand(Guid.Empty));
    }
}