using Bogus;
using BookManagement.Core.Domain.Entities.Categories;
using static BookManagement.Core.Domain.Entities.Categories.Specifications.CategorySpecification;

namespace BookManagement.Core.Domain.UnitTest.Entities.Categories;

[CollectionDefinition(nameof(CategoryTestsCollection))]
public class CategoryTestsCollection : ICollectionFixture<CategoryTestsFixture>
{
}

public class CategoryTestsFixture
{
    public Category GetValidCategory()
    {
        return new Faker<Category>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);

                return new Category(name, description);
            });
    }

    public Category GetValidCategoryWithParent()
    {
        return new Faker<Category>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize);
                var description = x.Random.String(1, DescriptionColumnSize);

                var parentName = x.Random.String2(NameColumnSize);
                var parentDescription = x.Random.String(1, DescriptionColumnSize);

                return new Category(name, description, new Category(parentName, parentDescription));
            });
    }

    public Category GetInvalidCategory()
    {
        return new Faker<Category>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String2(NameColumnSize, NameColumnSize * 2);
                var description = x.Random.String(DescriptionColumnSize + 1, DescriptionColumnSize * 2);

                return new Category(name, description);
            });
    }
}