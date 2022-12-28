using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.PublisherCommands;

public class DeletePublisherCommand : BaseCommand<DeletePublisherCommand, DeletePublisherCommandValidation>, ICommand
{
    public DeletePublisherCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class DeletePublisherCommandValidation : AbstractValidator<DeletePublisherCommand>
{
    public DeletePublisherCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}