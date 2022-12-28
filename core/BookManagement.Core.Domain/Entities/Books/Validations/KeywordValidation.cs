using BookManagement.Core.Domain.Entities.Books.Specifications;
using FluentValidation;

namespace BookManagement.Core.Domain.Entities.Books.Validations;

public class KeywordValidation : AbstractValidator<Keyword>
{
    public KeywordValidation()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(KeywordSpecification.DescriptionColumnSize);
    }
}