using BookManagement.Core.Domain.Handlers.AuthorHandlers;
using BookManagement.Core.Domain.Handlers.AuthorHandlers.Interfaces;
using BookManagement.Core.Domain.Handlers.BookHandlers;
using BookManagement.Core.Domain.Handlers.BookHandlers.Interfaces;
using BookManagement.Core.Domain.Handlers.CategoryHandlers;
using BookManagement.Core.Domain.Handlers.CategoryHandlers.Interfaces;
using BookManagement.Core.Domain.Handlers.PublisherHandlers;
using BookManagement.Core.Domain.Handlers.PublisherHandlers.Interfaces;
using BookManagement.Core.Domain.Queries.AuthorQueries;
using BookManagement.Core.Domain.Queries.BookQueries;
using BookManagement.Core.Domain.Queries.CategoryQueries;
using BookManagement.Core.Domain.Queries.PublisherQueries;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagement.Modules.Web.DependencyInjections;

public static class DomainDependencyInjections
{
    public static IServiceCollection AddDomainDependencies(this IServiceCollection services)
    {
        return services
            .AddScoped<IAuthorHandler, AuthorHandler>()
            .AddScoped<IBookHandler, BookHandler>()
            .AddScoped<ICategoryHandler, CategoryHandler>()
            .AddScoped<IPublisherHandler, PublisherHandler>()
            .AddScoped<IAuthorQueries, AuthorQueries>()
            .AddScoped<IBookQueries, BookQueries>()
            .AddScoped<ICategoryQueries, CategoryQueries>()
            .AddScoped<IPublisherQueries, PublisherQueries>();
    }
}