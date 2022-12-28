using BookManagement.Core.Domain.Entities.Authors.Specifications;
using FluentValidation;

namespace BookManagement.Core.Domain.Entities.Authors.Validations;

public class AuthorValidation : AbstractValidator<Author>
{
    public AuthorValidation()
    {
        RuleFor(x => x.Name).MaximumLength(AuthorSpecification.NameColumnSize);
        RuleFor(x => x.Description).MaximumLength(AuthorSpecification.DescriptionColumnSize);
    }
}