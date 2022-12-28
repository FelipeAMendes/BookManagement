using BookManagement.Core.Domain.Commands.AuthorCommands;
using BookManagement.Core.Domain.Handlers.AuthorHandlers.Interfaces;
using BookManagement.Core.Domain.Repositories.AuthorRepositories.Interfaces;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Handlers;

namespace BookManagement.Core.Domain.Handlers.AuthorHandlers;

public class AuthorHandler : BaseHandler, IAuthorHandler
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<ICommandResult<CommandNoneResult>> HandleAsync(CreateAuthorCommand createCommand)
    {
        var author = createCommand.ToEntity();

        AddErrors(author.Errors);

        if (!IsValid)
            return CreateErrorCommandResult(Errors);

        var authorCreated = await _authorRepository.CreateAsync(author);
        return CreateDefaultCommandResult(authorCreated, ValidationType.CreationError);
    }

    public async Task<ICommandResult<CommandNoneResult>> HandleAsync(UpdateAuthorCommand updateCommand)
    {
        var author = await _authorRepository.GetByIdAsync(updateCommand.Id);

        if (author is null)
            return CreateNotFoundCommandResult();

        author = updateCommand.UpdateEntity(author);

        AddErrors(author.Errors);

        if (!IsValid)
            return CreateErrorCommandResult(Errors);

        var authorUpdated = await _authorRepository.UpdateAsync(author);
        return CreateDefaultCommandResult(authorUpdated, ValidationType.ChangeError);
    }

    public async Task<ICommandResult<CommandNoneResult>> HandleAsync(DeleteAuthorCommand deleteCommand)
    {
        var author = await _authorRepository.GetByIdAsync(deleteCommand.Id);

        if (author is null)
            return CreateNotFoundCommandResult();

        author.Remove();

        var authorUpdated = await _authorRepository.UpdateAsync(author);
        return CreateDefaultCommandResult(authorUpdated, ValidationType.RemovalError);
    }
}