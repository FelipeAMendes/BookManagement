using BookManagement.Core.Domain.Entities.Categories.Specifications;
using BookManagement.Core.Domain.Entities.Publishers.Specifications;
using FluentValidation;

namespace BookManagement.Core.Domain.Entities.Categories.Validations;

public class CategoryValidation : AbstractValidator<Category>
{
    public CategoryValidation()
    {
        RuleFor(x => x.Name).MaximumLength(PublisherSpecification.NameColumnSize);
        RuleFor(x => x.Description).MaximumLength(CategorySpecification.DescriptionColumnSize);
    }
}