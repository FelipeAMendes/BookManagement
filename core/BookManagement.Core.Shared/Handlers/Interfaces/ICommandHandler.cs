using BookManagement.Core.Shared.Commands.Interfaces;

namespace BookManagement.Core.Shared.Handlers.Interfaces;

public interface ICommandHandler<TCommandResult, in TCommand> where TCommandResult : IResult where TCommand : ICommand
{
    Task<ICommandResult<TCommandResult>> HandleAsync(TCommand command);
}