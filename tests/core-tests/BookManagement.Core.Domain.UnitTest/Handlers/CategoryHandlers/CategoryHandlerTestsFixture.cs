using AutoFixture;
using AutoMapper;
using Bogus;
using BookManagement.Core.Domain.Commands.CategoryCommands;
using BookManagement.Core.Domain.Entities.Categories;
using BookManagement.Core.Domain.Handlers.CategoryHandlers;
using BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;
using BookManagement.Core.Domain.Repositories.CategoryRepositories.Interfaces;
using Moq;
using Moq.AutoMock;
using static BookManagement.Core.Domain.Entities.Categories.Specifications.CategorySpecification;

namespace BookManagement.Core.Domain.UnitTest.Handlers.CategoryHandlers;

[CollectionDefinition(nameof(CategoryHandlerTestsCollection))]
public class CategoryHandlerTestsCollection : ICollectionFixture<CategoryHandlerTestsFixture>
{
}

public class CategoryHandlerTestsFixture
{
    private readonly IFixture _autoFixture;
    private readonly AutoMocker _autoMocker;

    public CategoryHandlerTestsFixture()
    {
        _autoMocker = new AutoMocker();
        _autoFixture = new Fixture();
    }

    public CategoryHandler GetHandlerInstance()
    {
        return _autoMocker.CreateInstance<CategoryHandler>();
    }

    public CreateCategoryCommand GetValidCreateCommand(Guid? parentCategoryId)
    {
        var createCommand = _autoFixture.Create<CreateCategoryCommand>();
        createCommand.ParentCategoryId = parentCategoryId;
        return createCommand;
    }

    public CreateCategoryCommand GetInvalidCreateCommand()
    {
        return new Faker<CreateCategoryCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String(NameColumnSize + 1, NameColumnSize * 2);
                var description = x.Random.String(DescriptionColumnSize + 1, DescriptionColumnSize * 2);

                return new CreateCategoryCommand
                {
                    Name = name,
                    Description = description
                };
            });
    }

    public Mock<ICategoryRepository> GetCreateRepositoryMock(bool success)
    {
        var categoryRepository = _autoMocker.GetMock<ICategoryRepository>();

        categoryRepository
            .Setup(x => x.CreateAsync(It.IsAny<Category>()))
            .ReturnsAsync(success);

        return categoryRepository;
    }

    public Mock<ICategoryRepository> GetUpdateRepositoryMock(bool success)
    {
        var categoryRepository = _autoMocker.GetMock<ICategoryRepository>();

        categoryRepository
            .Setup(x => x.UpdateAsync(It.IsAny<Category>()))
            .ReturnsAsync(success);

        return categoryRepository;
    }

    public void CreateGetByIdRepositoryMock()
    {
        _autoMocker
            .GetMock<ICategoryRepository>()
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Category("CategoryName", "CategoryDescription"));
    }

    public void CreateNonExistingGetByIdRepositoryMock()
    {
        _autoMocker
            .GetMock<ICategoryRepository>()
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);
    }

    public void CreateQueryResultAutoMapperMock()
    {
        _autoMocker
            .GetMock<IMapper>()
            .Setup(x => x.Map<CategoryQueryResult>(It.IsAny<Category>()))
            .Returns(new CategoryQueryResult());
    }

    public UpdateCategoryCommand GetValidUpdateCommand(Guid? parentCategoryId)
    {
        var updateCommand = _autoFixture.Create<UpdateCategoryCommand>();
        updateCommand.ParentCategoryId = parentCategoryId;
        return updateCommand;
    }

    public UpdateCategoryCommand GetInvalidUpdateCommand()
    {
        return new Faker<UpdateCategoryCommand>()
            .CustomInstantiator(x =>
            {
                var name = x.Random.String(NameColumnSize + 1, NameColumnSize * 2);

                return new UpdateCategoryCommand
                {
                    Id = Guid.NewGuid(),
                    Name = name
                };
            });
    }

    public DeleteCategoryCommand GetValidDeleteCommand()
    {
        return _autoFixture.Create<DeleteCategoryCommand>();
    }
}