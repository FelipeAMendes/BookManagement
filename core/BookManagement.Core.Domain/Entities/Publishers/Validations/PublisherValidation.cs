using BookManagement.Core.Domain.Entities.Publishers.Specifications;
using FluentValidation;

namespace BookManagement.Core.Domain.Entities.Publishers.Validations;

public class PublisherValidation : AbstractValidator<Publisher>
{
    public PublisherValidation()
    {
        RuleFor(x => x.Name).MaximumLength(PublisherSpecification.NameColumnSize);
        RuleFor(x => x.Description).MaximumLength(PublisherSpecification.DescriptionColumnSize);
    }
}