using BookManagement.Core.Domain.Commands.BookCommands;
using BookManagement.Core.Domain.Handlers.BookHandlers.Interfaces;
using BookManagement.Core.Domain.Queries.BookQueries;
using BookManagement.Core.Domain.Queries.BookQueries.Inputs;
using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Modules.Web.Models.BookModels;
using BookManagement.Modules.Web.Services.BookServices.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Services.BookServices;

public class BookPageService : IBookPageService
{
    private readonly IBookHandler _bookHandler;
    private readonly IBookQueries _bookQueries;

    public BookPageService(IBookHandler bookHandler, IBookQueries bookQueries)
    {
        _bookHandler = bookHandler;
        _bookQueries = bookQueries;
    }

    public async Task<BookQueryResult> GetByIdAsync(Guid id)
    {
        var book = await _bookQueries.GetByIdAsync(id);

        return book;
    }

    public async Task<bool> BookExistsAsync(string isbn10, string isbn13)
    {
        return await _bookQueries.BookExistsAsync(isbn10, isbn13);
    }

    public async Task<IPaginateQueryResult<BookQueryResult>> GetPaginatedAsync(FilterBookQueries filter)
    {
        return await _bookQueries.GetPaginatedAsync(filter);
    }

    public async Task<ICommandResult<CommandNoneResult>> CreateAsync(BookModel bookModel)
    {
        var createCommand = new CreateBookCommand
        {
            Title = bookModel.Title,
            Description = bookModel.Description,
            CategoryId = bookModel.CategoryId,
            PublisherId = bookModel.PublisherId,
            Format = bookModel.Format,
            Isbn10 = bookModel.Isbn10,
            Isbn13 = bookModel.Isbn13,
            PublicationDate = bookModel.PublicationDate,
            Keywords = bookModel.Keywords.Select(x => x.ToCreateCommand()),
            Quotes = bookModel.Quotes.Select(x => x.ToCreateCommand()),
            Reviews = bookModel.Reviews.Select(x => x.ToCreateCommand())
        };

        var commandResult = await _bookHandler.HandleAsync(createCommand);

        return commandResult;
    }

    public async Task<ICommandResult<CommandNoneResult>> UpdateAsync(BookModel bookModel)
    {
        var updateCommand = new UpdateBookCommand
        {
            Id = bookModel.Id ?? Guid.Empty,
            Title = bookModel.Title,
            Description = bookModel.Description,
            CategoryId = bookModel.CategoryId,
            PublisherId = bookModel.PublisherId,
            Format = bookModel.Format,
            Isbn10 = bookModel.Isbn10,
            Isbn13 = bookModel.Isbn13,
            PublicationDate = bookModel.PublicationDate,
            Keywords = bookModel.Keywords.Select(x => x.ToUpdateCommand()),
            Quotes = bookModel.Quotes.Select(x => x.ToUpdateCommand()),
            Reviews = bookModel.Reviews.Select(x => x.ToUpdateCommand())
        };

        var commandResult = await _bookHandler.HandleAsync(updateCommand);

        return commandResult;
    }

    public async Task<ICommandResult<CommandNoneResult>> DeleteAsync(Guid id)
    {
        var deleteCommand = new DeleteBookCommand(id);

        var commandResult = await _bookHandler.HandleAsync(deleteCommand);

        return commandResult;
    }
}