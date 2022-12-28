using BookManagement.Core.Domain.Commands.BookCommands;
using BookManagement.Core.Domain.Handlers.BookHandlers.Extensions;
using BookManagement.Core.Domain.Handlers.BookHandlers.Interfaces;
using BookManagement.Core.Domain.Repositories.BookRepositories.Interfaces;
using BookManagement.Core.Domain.Repositories.CategoryRepositories.Interfaces;
using BookManagement.Core.Domain.Repositories.PublisherRepositories.Interfaces;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Handlers;

namespace BookManagement.Core.Domain.Handlers.BookHandlers;

public class BookHandler : BaseHandler, IBookHandler
{
    private readonly IBookRepository _bookRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPublisherRepository _publisherRepository;

    public BookHandler(IBookRepository bookRepository, ICategoryRepository categoryRepository, IPublisherRepository publisherRepository)
    {
        _bookRepository = bookRepository;
        _categoryRepository = categoryRepository;
        _publisherRepository = publisherRepository;
    }

    public async Task<ICommandResult<CommandNoneResult>> HandleAsync(CreateBookCommand createBookCommand)
    {
        var book = createBookCommand.ToEntity();

        await this.SetCategory(book, _categoryRepository, createBookCommand.CategoryId);
        await this.SetPublisher(book, _publisherRepository, createBookCommand.PublisherId);

        AddErrors(book.Errors);

        if (!IsValid)
            return CreateErrorCommandResult(Errors);

        var bookCreated = await _bookRepository.CreateAsync(book);
        return CreateDefaultCommandResult(bookCreated, ValidationType.CreationError);
    }

    public async Task<ICommandResult<CommandNoneResult>> HandleAsync(UpdateBookCommand updateCommand)
    {
        var book = await _bookRepository.GetByIdAsync(updateCommand.Id);

        if (book is null)
            return CreateNotFoundCommandResult();

        await this.SetCategory(book, _categoryRepository, updateCommand.CategoryId);
        await this.SetPublisher(book, _publisherRepository, updateCommand.PublisherId);

        book = updateCommand.UpdateEntity(book);

        AddErrors(book.Errors);

        if (!IsValid)
            return CreateErrorCommandResult(Errors);

        var bookUpdated = await _bookRepository.UpdateAsync(book);
        return CreateDefaultCommandResult(bookUpdated, ValidationType.ChangeError);
    }

    public async Task<ICommandResult<CommandNoneResult>> HandleAsync(DeleteBookCommand deleteBookCommand)
    {
        var book = await _bookRepository.GetByIdAsync(deleteBookCommand.Id);

        if (book is null)
            return CreateNotFoundCommandResult();

        book.Remove();

        var bookUpdated = await _bookRepository.UpdateAsync(book);
        return CreateDefaultCommandResult(bookUpdated, ValidationType.RemovalError);
    }
}