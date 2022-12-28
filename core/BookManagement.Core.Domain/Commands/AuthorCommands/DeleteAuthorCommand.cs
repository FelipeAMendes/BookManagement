using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.AuthorCommands;

public class DeleteAuthorCommand : BaseCommand<DeleteAuthorCommand, DeleteAuthorCommandValidation>, ICommand
{
    public DeleteAuthorCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class DeleteAuthorCommandValidation : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}