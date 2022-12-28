using BookManagement.Core.Domain.Commands.PublisherCommands;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Handlers.Interfaces;

namespace BookManagement.Core.Domain.Handlers.PublisherHandlers.Interfaces;

public interface IPublisherHandler :
    ICommandHandler<CommandNoneResult, CreatePublisherCommand>,
    ICommandHandler<CommandNoneResult, UpdatePublisherCommand>,
    ICommandHandler<CommandNoneResult, DeletePublisherCommand>
{
}