using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.BookCommands;

public class CreateBookCommand : BaseCommand<CreateBookCommand, CreateBookCommandValidation>, ICommand
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Format Format { get; set; }
    public DateTime? PublicationDate { get; set; }
    public string Isbn10 { get; set; }
    public string Isbn13 { get; set; }
    public Guid PublisherId { get; set; }
    public Guid CategoryId { get; set; }

    public IEnumerable<CreateBookKeywordCommand> Keywords { get; set; }
    public IEnumerable<CreateBookQuoteCommand> Quotes { get; set; }
    public IEnumerable<CreateBookReviewCommand> Reviews { get; set; }

    public Book ToEntity()
    {
        return new(Title, Description, PublicationDate, Format, Isbn10, Isbn13);
    }
}

public class CreateBookCommandValidation : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidation()
    {
        RuleFor(x => x.CategoryId).NotNull();
        RuleFor(x => x.PublisherId).NotNull();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(BookSpecification.TitleColumnSize);
        RuleFor(x => x.Description).MaximumLength(BookSpecification.DescriptionColumnSize);
        RuleFor(x => x.Format).IsInEnum();

        //TODO: Check it later...
        //RuleFor(x => x.Isbn10).Isbn10();
        //RuleFor(x => x.Isbn13).Isbn13();

        RuleForEach(x => x.Keywords).SetValidator(new CreateBookKeywordCommandValidation());
        RuleForEach(x => x.Quotes).SetValidator(new CreateBookQuoteCommandValidation());
        RuleForEach(x => x.Reviews).SetValidator(new CreateBookReviewCommandValidation());
    }
}