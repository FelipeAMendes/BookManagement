using BookManagement.Core.Domain.Entities.Books.Specifications;
using BookManagement.Core.Shared.Commands;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.BookCommands;

public class UpdateBookQuoteCommand : BaseCommand<UpdateBookQuoteCommand, UpdateBookQuoteCommandValidation>
{
    public Guid Id { get; set; }
    public string Description { get; set; }
}

public class UpdateBookQuoteCommandValidation : AbstractValidator<UpdateBookQuoteCommand>
{
    public UpdateBookQuoteCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(QuoteSpecification.DescriptionColumnSize);
    }
}