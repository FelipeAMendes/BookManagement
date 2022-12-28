using BookManagement.Core.Domain.Commands.BookCommands;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Handlers.Interfaces;

namespace BookManagement.Core.Domain.Handlers.BookHandlers.Interfaces;

public interface IBookHandler :
    ICommandHandler<CommandNoneResult, CreateBookCommand>,
    ICommandHandler<CommandNoneResult, UpdateBookCommand>,
    ICommandHandler<CommandNoneResult, DeleteBookCommand>
{
}