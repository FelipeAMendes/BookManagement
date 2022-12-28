using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.CategoryCommands;

public class DeleteCategoryCommand : BaseCommand<DeleteCategoryCommand, DeleteCategoryCommandValidation>, ICommand
{
    public DeleteCategoryCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class DeleteCategoryCommandValidation : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}