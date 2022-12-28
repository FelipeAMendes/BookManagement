using BookManagement.Core.Domain.Entities.Categories;
using BookManagement.Core.Domain.Entities.Categories.Specifications;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.CategoryCommands;

public class CreateCategoryCommand : BaseCommand<CreateCategoryCommand, CreateCategoryCommandValidation>, ICommand
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }

    public Category ToEntity()
    {
        return new(Name, Description);
    }
}

public class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(CategorySpecification.NameColumnSize);
        RuleFor(x => x.Description).MaximumLength(CategorySpecification.DescriptionColumnSize);
    }
}