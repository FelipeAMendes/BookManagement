using BookManagement.Core.Domain.Entities.Books.Specifications;
using BookManagement.Core.Shared.Commands;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.BookCommands;

public class UpdateBookKeywordCommand : BaseCommand<UpdateBookKeywordCommand, UpdateBookKeywordCommandValidation>
{
    public Guid Id { get; set; }
    public string Description { get; set; }
}

public class UpdateBookKeywordCommandValidation : AbstractValidator<UpdateBookKeywordCommand>
{
    public UpdateBookKeywordCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(KeywordSpecification.DescriptionColumnSize);
    }
}