using BookManagement.Core.Domain.Entities.Books.Specifications;
using FluentValidation;

namespace BookManagement.Core.Domain.Entities.Books.Validations;

public class QuoteValidation : AbstractValidator<Quote>
{
    public QuoteValidation()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(QuoteSpecification.DescriptionColumnSize);
    }
}