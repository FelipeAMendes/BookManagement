using BookManagement.Core.Domain.Commands.PublisherCommands;
using BookManagement.Core.Domain.Handlers.PublisherHandlers.Interfaces;
using BookManagement.Core.Domain.Repositories.PublisherRepositories.Interfaces;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Handlers;

namespace BookManagement.Core.Domain.Handlers.PublisherHandlers;

public class PublisherHandler : BaseHandler, IPublisherHandler
{
    private readonly IPublisherRepository _publisherRepository;

    public PublisherHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<ICommandResult<CommandNoneResult>> HandleAsync(CreatePublisherCommand createCommand)
    {
        var publisher = createCommand.ToEntity();

        AddErrors(publisher.Errors);

        if (!IsValid)
            return CreateErrorCommandResult(Errors);

        var publisherCreated = await _publisherRepository.CreateAsync(publisher);
        return CreateDefaultCommandResult(publisherCreated, ValidationType.CreationError);
    }

    public async Task<ICommandResult<CommandNoneResult>> HandleAsync(UpdatePublisherCommand updateCommand)
    {
        var publisher = await _publisherRepository.GetByIdAsync(updateCommand.Id);

        if (publisher is null)
            return CreateNotFoundCommandResult();

        updateCommand.UpdateEntity(publisher);

        AddErrors(publisher.Errors);

        if (!IsValid)
            return CreateErrorCommandResult(Errors);

        var publisherUpdated = await _publisherRepository.UpdateAsync(publisher);
        return CreateDefaultCommandResult(publisherUpdated, ValidationType.ChangeError);
    }

    public async Task<ICommandResult<CommandNoneResult>> HandleAsync(DeletePublisherCommand deleteCommand)
    {
        var publisher = await _publisherRepository.GetByIdAsync(deleteCommand.Id);

        if (publisher is null)
            return CreateNotFoundCommandResult();

        publisher.Remove();

        var publisherUpdated = await _publisherRepository.UpdateAsync(publisher);
        return CreateDefaultCommandResult(publisherUpdated, ValidationType.RemovalError);
    }
}