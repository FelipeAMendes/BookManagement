using BookManagement.Core.Domain.Entities.Books.Specifications;
using FluentValidation;

namespace BookManagement.Core.Domain.Entities.Books.Validations;

public class BookValidation : AbstractValidator<Book>
{
    public BookValidation()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(BookSpecification.TitleColumnSize);
        RuleFor(x => x.Description).MaximumLength(BookSpecification.DescriptionColumnSize);
        RuleFor(x => x.Format).IsInEnum();
    }
}