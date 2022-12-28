using BookManagement.Core.Domain.Commands.AuthorCommands;
using BookManagement.Core.Domain.Handlers.AuthorHandlers.Interfaces;
using BookManagement.Core.Domain.Queries.AuthorQueries;
using BookManagement.Core.Domain.Queries.AuthorQueries.Inputs;
using BookManagement.Core.Domain.Queries.AuthorQueries.QueryResults;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Modules.Web.Models.AuthorModels;
using BookManagement.Modules.Web.Services.AuthorServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Services.AuthorServices;

public class AuthorPageService : IAuthorPageService
{
    private readonly IAuthorHandler _authorHandler;
    private readonly IAuthorQueries _authorQueries;

    public AuthorPageService(IAuthorHandler authorHandler, IAuthorQueries authorQueries)
    {
        _authorHandler = authorHandler;
        _authorQueries = authorQueries;
    }

    public async Task<ICollection<AuthorQueryResult>> GetAllAsync()
    {
        var authors = await _authorQueries.GetAllAsync();

        return authors;
    }

    public async Task<AuthorQueryResult> GetByIdAsync(Guid id)
    {
        var author = await _authorQueries.GetByIdAsync(id);

        return author;
    }

    public async Task<bool> AuthorExistsAsync(string name)
    {
        return await _authorQueries.AuthorExistsAsync(name);
    }

    public async Task<IPaginateQueryResult<AuthorQueryResult>> GetPaginatedAsync(FilterAuthorQueries filter)
    {
        return await _authorQueries.GetPaginatedAsync(filter);
    }

    public async Task<ICommandResult<CommandNoneResult>> CreateAsync(AuthorModel authorModel)
    {
        var createCommand = new CreateAuthorCommand
        {
            Name = authorModel.Name,
            Description = authorModel.Description
        };

        var commandResult = await _authorHandler.HandleAsync(createCommand);

        return commandResult;
    }

    public async Task<ICommandResult<CommandNoneResult>> UpdateAsync(AuthorModel authorModel)
    {
        var updateCommand = new UpdateAuthorCommand
        {
            Id = authorModel.Id ?? Guid.Empty,
            Name = authorModel.Name,
            Description = authorModel.Description
        };

        var commandResult = await _authorHandler.HandleAsync(updateCommand);

        return commandResult;
    }

    public async Task<ICommandResult<CommandNoneResult>> DeleteAsync(Guid id)
    {
        var deleteAuthorCommand = new DeleteAuthorCommand(id);

        var commandResult = await _authorHandler.HandleAsync(deleteAuthorCommand);

        return commandResult;
    }
}