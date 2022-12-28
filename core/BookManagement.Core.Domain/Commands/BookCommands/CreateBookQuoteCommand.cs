using BookManagement.Core.Domain.Entities.Books.Specifications;
using BookManagement.Core.Shared.Commands;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.BookCommands;

public class CreateBookQuoteCommand : BaseCommand<CreateBookQuoteCommand, CreateBookQuoteCommandValidation>
{
    public string Description { get; set; }
}

public class CreateBookQuoteCommandValidation : AbstractValidator<CreateBookQuoteCommand>
{
    public CreateBookQuoteCommandValidation()
    {
        RuleFor(x => x.Description).MaximumLength(QuoteSpecification.DescriptionColumnSize);
    }
}