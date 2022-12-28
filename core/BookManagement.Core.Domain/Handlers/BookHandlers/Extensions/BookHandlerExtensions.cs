using BookManagement.Core.Domain.Commands.BookCommands;
using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Repositories.CategoryRepositories.Interfaces;
using BookManagement.Core.Domain.Repositories.PublisherRepositories.Interfaces;
using BookManagement.Core.Shared.Extensions.EnumExtensions;
using BookManagement.Core.Shared.Extensions.ObjectExtensions;
using BookManagement.Core.Shared.Handlers;

namespace BookManagement.Core.Domain.Handlers.BookHandlers.Extensions;

public static class BookHandlerExtensions
{
    public static async Task SetCategory(this BookHandler bookHandler, Book book,
        ICategoryRepository categoryRepository, Guid categoryId)
    {
        var sameCategory = book.Category is not null && book.Category.Id == categoryId;
        if (sameCategory)
            return;

        var category = await categoryRepository.GetByIdAsync(categoryId);
        if (category.IsNull())
        {
            bookHandler.AddError<CreateBookCommand>(x => x.CategoryId, ValidationType.ItemNotFound.GetDescription());
            return;
        }

        book.SetCategory(category);
    }

    public static async Task SetPublisher(this BookHandler bookHandler, Book book,
        IPublisherRepository publisherRepository, Guid publisherId)
    {
        var samePublisher = book.Publisher is not null && book.Publisher.Id == publisherId;
        if (samePublisher)
            return;

        var publisher = await publisherRepository.GetByIdAsync(publisherId);
        if (publisher.IsNull())
        {
            bookHandler.AddError<CreateBookCommand>(x => x.PublisherId, ValidationType.ItemNotFound.GetDescription());
            return;
        }

        book.SetPublisher(publisher);
    }
}