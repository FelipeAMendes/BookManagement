using Bogus;
using BookManagement.Core.Domain.Commands.CategoryCommands;
using static BookManagement.Core.Domain.Entities.Categories.Specifications.CategorySpecification;

namespace BookManagement.Core.Domain.UnitTest.Commands.CategoryCommands;

[CollectionDefinition(nameof(CategoryCommandTestsCollection))]
public class CategoryCommandTestsCollection : ICollectionFixture<CategoryCommandTestsFixture>
{
}

public class CategoryCommandTestsFixture
{
    public CreateCategoryCommand GetValidCreateCategoryCommand()
    {
        return new Faker<CreateCategoryCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);

                return new CreateCategoryCommand
                {
                    Name = name,
                    Description = description
                };
            });
    }

    public CreateCategoryCommand GetInvalidCreateCategoryCommand()
    {
        return new Faker<CreateCategoryCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize + 1, NameColumnSize * 2);
                var description = x.Random.String2(DescriptionColumnSize + 1, DescriptionColumnSize * 2);

                return new CreateCategoryCommand
                {
                    Name = name,
                    Description = description
                };
            });
    }

    public UpdateCategoryCommand GetValidUpdateCategoryCommand()
    {
        return new Faker<UpdateCategoryCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);

                return new UpdateCategoryCommand
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description
                };
            });
    }

    public UpdateCategoryCommand GetInvalidUpdateCategoryCommand()
    {
        return new Faker<UpdateCategoryCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize + 1, NameColumnSize * 2);
                var description = x.Random.String2(DescriptionColumnSize + 1, DescriptionColumnSize * 2);

                return new UpdateCategoryCommand
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description
                };
            });
    }

    public DeleteCategoryCommand GetValidDeleteCategoryCommand()
    {
        return new Faker<DeleteCategoryCommand>()
            .CustomInstantiator(_ => new DeleteCategoryCommand(Guid.NewGuid()));
    }

    public DeleteCategoryCommand GetInvalidDeleteCategoryCommand()
    {
        return new Faker<DeleteCategoryCommand>()
            .CustomInstantiator(_ => new DeleteCategoryCommand(Guid.Empty));
    }
}