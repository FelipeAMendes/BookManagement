using BookManagement.Core.Domain.Entities.Books.Specifications;
using BookManagement.Core.Shared.Commands;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.BookCommands;

public class CreateBookKeywordCommand : BaseCommand<CreateBookKeywordCommand, CreateBookKeywordCommandValidation>
{
    public string Description { get; set; }
}

public class CreateBookKeywordCommandValidation : AbstractValidator<CreateBookKeywordCommand>
{
    public CreateBookKeywordCommandValidation()
    {
        RuleFor(x => x.Description).MaximumLength(KeywordSpecification.DescriptionColumnSize);
    }
}