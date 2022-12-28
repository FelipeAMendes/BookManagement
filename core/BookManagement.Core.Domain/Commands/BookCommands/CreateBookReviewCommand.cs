using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using BookManagement.Core.Shared.Commands;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.BookCommands;

public class CreateBookReviewCommand : BaseCommand<CreateBookReviewCommand, CreateBookReviewCommandValidation>
{
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public string AuthorNameInfo { get; set; }
    public ReviewType ReviewType { get; set; }
}

public class CreateBookReviewCommandValidation : AbstractValidator<CreateBookReviewCommand>
{
    public CreateBookReviewCommandValidation()
    {
        RuleFor(x => x.AuthorName).NotEmpty().MaximumLength(ReviewSpecification.AuthorNameColumnSize);
        RuleFor(x => x.AuthorNameInfo).MaximumLength(ReviewSpecification.AuthorNameInfoColumnSize);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(ReviewSpecification.DescriptionColumnSize);
        RuleFor(x => x.ReviewType).IsInEnum();
    }
}