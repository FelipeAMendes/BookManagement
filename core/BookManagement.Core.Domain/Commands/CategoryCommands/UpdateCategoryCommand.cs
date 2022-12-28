using BookManagement.Core.Domain.Entities.Categories;
using BookManagement.Core.Domain.Entities.Categories.Specifications;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.CategoryCommands;

public class UpdateCategoryCommand : BaseCommand<UpdateCategoryCommand, UpdateCategoryCommandValidation>, ICommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }

    public Category UpdateEntity(Category category)
    {
        category.Edit(Name, Description);
        return category;
    }
}

public class UpdateCategoryCommandValidation : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(CategorySpecification.NameColumnSize);
        RuleFor(x => x.Description).MaximumLength(CategorySpecification.DescriptionColumnSize);
    }
}