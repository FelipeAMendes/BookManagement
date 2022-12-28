using BookManagement.Core.Domain.Commands.CategoryCommands;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Handlers.Interfaces;

namespace BookManagement.Core.Domain.Handlers.CategoryHandlers.Interfaces;

public interface ICategoryHandler :
    ICommandHandler<CommandNoneResult, CreateCategoryCommand>,
    ICommandHandler<CommandNoneResult, UpdateCategoryCommand>,
    ICommandHandler<CommandNoneResult, DeleteCategoryCommand>
{
}