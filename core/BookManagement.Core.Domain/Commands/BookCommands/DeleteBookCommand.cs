using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.BookCommands;

public class DeleteBookCommand : BaseCommand<DeleteBookCommand, DeleteBookCommandValidation>, ICommand
{
    public DeleteBookCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class DeleteBookCommandValidation : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}