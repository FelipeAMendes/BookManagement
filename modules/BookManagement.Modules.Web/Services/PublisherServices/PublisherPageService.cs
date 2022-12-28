using BookManagement.Core.Domain.Commands.PublisherCommands;
using BookManagement.Core.Domain.Handlers.PublisherHandlers.Interfaces;
using BookManagement.Core.Domain.Queries.PublisherQueries;
using BookManagement.Core.Domain.Queries.PublisherQueries.Inputs;
using BookManagement.Core.Domain.Queries.PublisherQueries.QueryResults;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Modules.Web.Models.PublisherModels;
using BookManagement.Modules.Web.Services.PublisherServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Services.PublisherServices;

public class PublisherPageService : IPublisherPageService
{
    private readonly IPublisherHandler _publisherHandler;
    private readonly IPublisherQueries _publisherQueries;

    public PublisherPageService(IPublisherHandler publisherHandler, IPublisherQueries publisherQueries)
    {
        _publisherHandler = publisherHandler;
        _publisherQueries = publisherQueries;
    }

    public async Task<ICollection<PublisherQueryResult>> GetAllAsync()
    {
        var publishers = await _publisherQueries.GetAllAsync();

        return publishers;
    }

    public async Task<PublisherQueryResult> GetByIdAsync(Guid id)
    {
        var publisher = await _publisherQueries.GetByIdAsync(id);

        return publisher;
    }

    public async Task<IPaginateQueryResult<PublisherQueryResult>> GetPaginatedAsync(FilterPublisherQueries filter)
    {
        var publishers = await _publisherQueries.GetPaginatedAsync(filter);

        return publishers;
    }

    public async Task<bool> PublisherExistsAsync(string name)
    {
        return await _publisherQueries.PublisherExistsAsync(name);
    }

    public async Task<ICommandResult<CommandNoneResult>> CreateAsync(PublisherModel publisherModel)
    {
        var createCommand = new CreatePublisherCommand
        {
            Name = publisherModel.Name,
            Description = publisherModel.Description
        };

        var commandResult = await _publisherHandler.HandleAsync(createCommand);

        return commandResult;
    }

    public async Task<ICommandResult<CommandNoneResult>> UpdateAsync(PublisherModel publisherModel)
    {
        var updateCommand = new UpdatePublisherCommand
        {
            Id = publisherModel.Id ?? Guid.Empty,
            Name = publisherModel.Name,
            Description = publisherModel.Description
        };

        var commandResult = await _publisherHandler.HandleAsync(updateCommand);

        return commandResult;
    }

    public async Task<ICommandResult<CommandNoneResult>> DeleteAsync(Guid id)
    {
        var deleteCommand = new DeletePublisherCommand(id);

        var commandResult = await _publisherHandler.HandleAsync(deleteCommand);

        return commandResult;
    }
}