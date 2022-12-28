using BookManagement.Modules.Web.DependencyInjections;
using BookManagement.Modules.Web.PageFilters;
using BookManagement.Modules.Web.Services.AuthorServices;
using BookManagement.Modules.Web.Services.AuthorServices.Interfaces;
using BookManagement.Modules.Web.Services.BookServices;
using BookManagement.Modules.Web.Services.BookServices.Interfaces;
using BookManagement.Modules.Web.Services.CategoryServices;
using BookManagement.Modules.Web.Services.CategoryServices.Interfaces;
using BookManagement.Modules.Web.Services.PublisherServices;
using BookManagement.Modules.Web.Services.PublisherServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagement.Modules.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext(configuration)
            .AddInfraDependencies()
            .AddDomainDependencies()
            .AddScoped<TransactionPageFilter>();
    }

    public static IServiceCollection AddPageServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorPageService, AuthorPageService>();
        services.AddScoped<IBookPageService, BookPageService>();
        services.AddScoped<ICategoryPageService, CategoryPageService>();
        services.AddScoped<IPublisherPageService, PublisherPageService>();

        return services;
    }
}