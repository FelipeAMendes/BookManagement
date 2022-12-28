using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.BookCommands;

public class UpdateBookCommand : BaseCommand<UpdateBookCommand, UpdateBookCommandValidation>, ICommand
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Format Format { get; set; }
    public DateTime? PublicationDate { get; set; }
    public string Isbn10 { get; set; }
    public string Isbn13 { get; set; }
    public Guid PublisherId { get; set; }
    public Guid CategoryId { get; set; }

    public IEnumerable<UpdateBookKeywordCommand> Keywords { get; set; }
    public IEnumerable<UpdateBookQuoteCommand> Quotes { get; set; }
    public IEnumerable<UpdateBookReviewCommand> Reviews { get; set; }

    public Book UpdateEntity(Book book)
    {
        book.Edit(Title, Description, PublicationDate, Format, Isbn10, Isbn13);
        return book;
    }
}

public class UpdateBookCommandValidation : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.CategoryId).NotNull();
        RuleFor(x => x.PublisherId).NotNull();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(BookSpecification.TitleColumnSize);
        RuleFor(x => x.Description).MaximumLength(BookSpecification.DescriptionColumnSize);
        RuleFor(x => x.Format).IsInEnum();
    }
}