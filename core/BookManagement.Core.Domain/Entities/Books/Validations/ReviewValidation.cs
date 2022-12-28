using BookManagement.Core.Domain.Entities.Books.Specifications;
using FluentValidation;

namespace BookManagement.Core.Domain.Entities.Books.Validations;

public class ReviewValidation : AbstractValidator<Review>
{
    public ReviewValidation()
    {
        RuleFor(x => x.AuthorName).NotEmpty().MaximumLength(ReviewSpecification.AuthorNameColumnSize);
        RuleFor(x => x.AuthorNameInfo).MaximumLength(ReviewSpecification.AuthorNameInfoColumnSize);
        RuleFor(x => x.ReviewType).IsInEnum();
        RuleFor(x => x.Description).NotEmpty().MaximumLength(ReviewSpecification.DescriptionColumnSize);
    }
}