using BookManagement.Core.Domain.Commands.AuthorCommands;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Handlers.Interfaces;

namespace BookManagement.Core.Domain.Handlers.AuthorHandlers.Interfaces;

public interface IAuthorHandler :
    ICommandHandler<CommandNoneResult, CreateAuthorCommand>,
    ICommandHandler<CommandNoneResult, UpdateAuthorCommand>,
    ICommandHandler<CommandNoneResult, DeleteAuthorCommand>
{
}